using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllInSkateChallenge.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Display Name")]
            [Required]
            public string SkaterName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Send me emails about my progress in the ALL IN Skate Challenge.")]
            public bool AcceptProgressNotifications { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                SkaterName = user.SkaterName,
                AcceptProgressNotifications = user.AcceptProgressNotifications
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumberChanged = !string.Equals(Input.PhoneNumber, user.PhoneNumber);
            var skaterNameChanged = !string.Equals(Input.SkaterName, user.SkaterName);
            var acceptProgressChanged = !Input.AcceptProgressNotifications.Equals(user.AcceptProgressNotifications);
            if (phoneNumberChanged || skaterNameChanged || acceptProgressChanged)
            {
                user.PhoneNumber = Input.PhoneNumber;
                user.SkaterName = Input.SkaterName;
                user.AcceptProgressNotifications = Input.AcceptProgressNotifications;

                var saveResult = await _userManager.UpdateAsync(user);
                if (!saveResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to save user name.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
