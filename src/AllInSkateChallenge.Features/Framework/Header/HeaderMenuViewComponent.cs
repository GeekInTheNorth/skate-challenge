namespace AllInSkateChallenge.Features.Framework.Header
{
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Gravatar;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IGravatarResolver gravatarResolver;

        public HeaderMenuViewComponent(UserManager<ApplicationUser> userManager, IGravatarResolver gravatarResolver)
        {
            this.userManager = userManager;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userDetails = await userManager.GetUserAsync(UserClaimsPrincipal);

            var model = new HeaderMenuViewModel
                            {
                                UserName = userDetails?.SkaterName,
                                UserProfileImage =
                                    string.IsNullOrWhiteSpace(userDetails?.ExternalProfileImage)
                                        ? gravatarResolver.GetGravatarUrl(userDetails?.Email)
                                        : userDetails?.ExternalProfileImage,
                                IsLoggedIn = userDetails != null
                            };

            return View("~/Views/Shared/Components/HeaderMenu/Default.cshtml", model);
        }
    }
}
