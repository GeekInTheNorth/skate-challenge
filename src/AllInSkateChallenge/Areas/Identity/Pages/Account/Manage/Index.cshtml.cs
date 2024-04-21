namespace AllInSkateChallenge.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Common;
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Kontent;
    using AllInSkateChallenge.Features.Gravatar;
    using AllInSkateChallenge.Features.Services.BlobStorage;
    using AllInSkateChallenge.Features.Validators;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Options;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICheckPointRepository checkPointRepository;
        private readonly IGravatarResolver gravatarResolver;
        private readonly IBlobStorageService blobStorageService;
        private readonly ChallengeSettings challengeSettings;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICheckPointRepository checkPointRepository, 
            IGravatarResolver gravatarResolver,
            IBlobStorageService blobStorageService,
            IOptions<ChallengeSettings> challengeSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.checkPointRepository = checkPointRepository;
            this.gravatarResolver = gravatarResolver;
            this.blobStorageService = blobStorageService;
            this.challengeSettings = challengeSettings.Value;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ProfileImage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SelectListItem> SkateTargets => checkPointRepository.GetSelectList();

        public bool IsTeamEvent => challengeSettings.ChallengeMode == ChallengeMode.Team;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.ImageRemoved && Input.ProfileImage == null)
            {
                await blobStorageService.DeleteFile(user.ExternalProfileImage);
                user.ExternalProfileImage = null;
            }

            if (Input.ProfileImage != null)
            {
                using (var stream = Input.ProfileImage.OpenReadStream())
                {
                    var fileName = $"{user.Id}{Path.GetExtension(Input.ProfileImage.FileName)}";
                    user.ExternalProfileImage = await blobStorageService.StoreFile(fileName, stream, Input.ProfileImage.ContentType);
                }
            }

            user.SkaterName = Input.SkaterName;
            user.AcceptProgressNotifications = Input.AcceptProgressNotifications;
            user.Target = Input.Target;

            if (IsTeamEvent)
            {
                user.Target = checkPointRepository.GetGoalCheckpoints().LastOrDefault()?.SkateTarget ?? Input.Target;
            }

            var saveResult = await userManager.UpdateAsync(user);
            if (!saveResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to save user name.";
                return RedirectToPage();
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await userManager.GetUserNameAsync(user);
            Username = userName;
            ProfileImage = user.ExternalProfileImage;

            Input = new InputModel
            {
                GravatarUrl = gravatarResolver.GetGravatarUrl(user.Email),
                SkaterName = user.SkaterName,
                AcceptProgressNotifications = user.AcceptProgressNotifications,
                Target = user.Target
            };

            if (IsTeamEvent)
            {
                Input.Target = checkPointRepository.GetGoalCheckpoints().LastOrDefault()?.SkateTarget ?? Input.Target;
            }
        }

        public class InputModel
        {
            [Display(Name = "Gravatar")]
            public string GravatarUrl { get; set; }

            [Display(Name = "Display Name")]
            [Required]
            public string SkaterName { get; set; }

            [Display(Name = "Your Personal Target")]
            public int Target { get; set; }

            [Display(Name = "Send me emails about my progress in the Roller Girl Gang Virtual Skate Marathon.")]
            public bool AcceptProgressNotifications { get; set; }

            [Display(Name = "Profile Image")]
            [MaxFileSize(1048576, ErrorMessage = "The selected image exceeds the file size limit of 1mb.")]
            [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
            public IFormFile ProfileImage { get; set; }

            public bool ImageRemoved { get; set; }
        }
    }
}
