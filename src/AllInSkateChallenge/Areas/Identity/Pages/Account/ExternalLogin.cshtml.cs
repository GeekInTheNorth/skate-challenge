using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Skater.Registration;
using AllInSkateChallenge.Features.Strava;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IMediator _mediator;
        private readonly ICheckPointRepository _checkPointRepository;
        private readonly ChallengeSettings _challengeSettings;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IMediator mediator,
            ICheckPointRepository checkPointRepository,
            IOptions<ChallengeSettings> challengeSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _mediator = mediator;
            _checkPointRepository = checkPointRepository;
            _challengeSettings = challengeSettings.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool IsRegistrationOver { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IList<SelectListItem> SkateTargets => _checkPointRepository.GetSelectList();

        public bool IsTeamEvent => _challengeSettings.ChallengeMode == ChallengeMode.Team;

        public class InputModel : IValidatableObject
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email Address (Some third parties such as Strava do not share this data)")]
            public string Email { get; set; }

            [Display(Name = "Your Personal Target")]
            public int Target { get; set; }

            [Display(Name = "Send me emails about my progress in the Roller Girl Gang Virtual Skate Marathon.")]
            public bool AcceptProgressNotifications { get; set; }

            [Display(Name = "I agree to the Terms & Conditions of this event.")]
            public bool AcceptTermsAndConditions { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!AcceptTermsAndConditions)
                {
                    yield return new ValidationResult("You must accept the terms and conditions to partake in this event.", new[] { nameof(AcceptTermsAndConditions) });
                }
            }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(
            [FromForm] string provider, 
            [FromForm] bool rememberStrava = false, 
            [FromQuery] string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl, rememberStrava });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null, bool rememberStrava = false)
        {
            IsRegistrationOver = await _mediator.Send(new RegistrationAvailabilityQuery());

            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: rememberStrava, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                var applicationUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                foreach(var authenticationToken in info.AuthenticationTokens)
                {
                    await _userManager.SetAuthenticationTokenAsync(applicationUser, info.LoginProvider, authenticationToken.Name, authenticationToken.Value);
                }

                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                Input = new InputModel
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Target = _checkPointRepository.GetGoalCheckpoints().LastOrDefault()?.SkateTarget ?? 0
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            IsRegistrationOver = await _mediator.Send(new RegistrationAvailabilityQuery());

            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid && !IsRegistrationOver)
            {
                var user = new ApplicationUser 
                { 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    IsStravaAccount = info.LoginProvider.Equals(StravaConstants.ProviderName, System.StringComparison.CurrentCultureIgnoreCase),
                    AcceptProgressNotifications = Input.AcceptProgressNotifications,
                    Target = Input.Target
                };

                if (!info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.Email)) && info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.NameIdentifier)))
                {
                    user.UserName = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                if (info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.Name)))
                {
                    user.SkaterName = info.Principal.FindFirstValue(ClaimTypes.Name);
                }
                else if (info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.GivenName)) && info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.Surname)))
                {
                    user.SkaterName = string.Format("{0} {1}", info.Principal.FindFirstValue(ClaimTypes.GivenName), info.Principal.FindFirstValue(ClaimTypes.Surname)).Trim();
                }
                else if (info.Principal.HasClaim(x => x.Type.Equals(ClaimTypes.GivenName)))
                {
                    user.SkaterName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                }

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        var fromSkateEverywhere = Request.Cookies.Any(x => x.Key.Equals("FromSkateEverywhere") && x.Value.Equals("true"));
                        var command = new SendRegistrationEmailCommand { Email = Input.Email, EmailConfirmationUrl = callbackUrl, FromSkateEverywhere = fromSkateEverywhere };
                        await _mediator.Send(command);

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
