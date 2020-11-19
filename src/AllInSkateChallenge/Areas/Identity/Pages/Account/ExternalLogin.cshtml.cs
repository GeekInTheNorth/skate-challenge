using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.Registration;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IMediator _mediator;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender, 
            IMediator mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _mediator = mediator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel : IValidatableObject
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email Address (Some third parties such as Strava do not share this data)")]
            public string Email { get; set; }

            [Display(Name = "Send me emails about my progress in the ALL IN Skate Challenge.")]
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

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
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
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
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
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    IsStravaAccount = info.LoginProvider.Equals("Strava", System.StringComparison.CurrentCultureIgnoreCase),
                    AcceptProgressNotifications = Input.AcceptProgressNotifications
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

                        var command = new SendRegistrationEmailCommand { Email = Input.Email, EmailConfirmationUrl = callbackUrl };
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
