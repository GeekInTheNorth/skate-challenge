using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Skater.Registration;

using MediatR;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMediator _mediator;
        private readonly ICheckPointRepository _checkPointRepository;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IMediator mediator, 
            ICheckPointRepository checkPointRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            _checkPointRepository = checkPointRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool IsRegistrationOver { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public IList<SelectListItem> SkateTargets => _checkPointRepository.GetSelectList();

        public class InputModel : IValidatableObject
        {
            [Required]
            [Display(Name = "Display Name")]
            public string SkaterName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Your Personal Target")]
            public SkateTarget Target { get; set; }

            [Display(Name = "Send me emails about my progress in the Roller Girl Gang Skate Challenge.")]
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

        public async Task OnGetAsync(string returnUrl = null, string referrer = null)
        {
            if (string.Equals(referrer, "SEP", System.StringComparison.CurrentCultureIgnoreCase))
            {
                Response.Cookies.Append("FromSkateEverywhere", "true");
            }

            Input = new InputModel { Target = SkateTarget.LiverpoolCanningDock };
            IsRegistrationOver = await _mediator.Send(new RegistrationAvailabilityQuery());
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            IsRegistrationOver = await _mediator.Send(new RegistrationAvailabilityQuery());
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    SkaterName = Input.SkaterName, 
                    AcceptProgressNotifications = Input.AcceptProgressNotifications,
                    Target = Input.Target
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    var fromSkateEverywhere = Request.Cookies.Any(x => x.Key.Equals("FromSkateEverywhere") && x.Value.Equals("true"));
                    var command = new SendRegistrationEmailCommand { Email = Input.Email, EmailConfirmationUrl = callbackUrl, FromSkateEverywhere = fromSkateEverywhere };
                    await _mediator.Send(command);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
