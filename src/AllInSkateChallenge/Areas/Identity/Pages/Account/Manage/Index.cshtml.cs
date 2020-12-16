namespace AllInSkateChallenge.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Static;
    using AllInSkateChallenge.Features.Gravatar;
    using AllInSkateChallenge.Features.Services.BlobStorage;
    using AllInSkateChallenge.Features.Validators;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICheckPointRepository _checkPointRepository;
        private readonly IGravatarResolver _gravatarResolver;
        private readonly IBlobStorageService _blobStorageService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICheckPointRepository checkPointRepository, 
            IGravatarResolver gravatarResolver,
            IBlobStorageService blobStorageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _checkPointRepository = checkPointRepository;
            _gravatarResolver = gravatarResolver;
            _blobStorageService = blobStorageService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ProfileImage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SelectListItem> SkateTargets => _checkPointRepository.GetSelectList();

        public class InputModel
        {
            [Display(Name = "Gravatar")]
            public string GravatarUrl { get; set; }

            [Display(Name = "Display Name")]
            [Required]
            public string SkaterName { get; set; }

            [Display(Name = "Your Personal Target")]
            public SkateTarget Target { get; set; }

            [Display(Name = "Send me emails about my progress in the ALL IN Skate Challenge.")]
            public bool AcceptProgressNotifications { get; set; }

            [Display(Name = "Profile Image")]
            [MaxFileSize(4194304)]
            [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
            public IFormFile ProfileImage { get; set; }
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

            var profileImagePath = string.Empty;
            if (Input.ProfileImage != null)
            {
                using (var stream = Input.ProfileImage.OpenReadStream())
                {
                    var fileName = $"{user.Id}{Path.GetExtension(Input.ProfileImage.FileName)}";
                    profileImagePath = await _blobStorageService.StoreFile(fileName, stream, Input.ProfileImage.ContentType);
                }
            }

            var skaterNameChanged = !string.Equals(Input.SkaterName, user.SkaterName);
            var targetChanged = !Input.Target.Equals(user.Target);
            var acceptProgressChanged = !Input.AcceptProgressNotifications.Equals(user.AcceptProgressNotifications);
            var profileImageChanged = Input.ProfileImage != null && !string.Equals(user.ExternalProfileImage, profileImagePath, StringComparison.CurrentCultureIgnoreCase);
            if (skaterNameChanged || acceptProgressChanged || targetChanged || profileImageChanged)
            {
                user.SkaterName = Input.SkaterName;
                user.AcceptProgressNotifications = Input.AcceptProgressNotifications;
                user.Target = Input.Target;
                user.ExternalProfileImage = profileImageChanged ? profileImagePath : user.ExternalProfileImage;

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

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;
            ProfileImage = user.ExternalProfileImage;

            Input = new InputModel
                        {
                            GravatarUrl = _gravatarResolver.GetGravatarUrl(user.Email),
                            SkaterName = user.SkaterName,
                            AcceptProgressNotifications = user.AcceptProgressNotifications,
                            Target = user.Target
                        };
        }
    }
}
