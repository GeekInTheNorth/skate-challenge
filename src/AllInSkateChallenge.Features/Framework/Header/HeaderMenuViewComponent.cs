namespace AllInSkateChallenge.Features.Framework.Header
{
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Gravatar;

    using Microsoft.AspNetCore.Mvc;

    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly IGravatarResolver gravatarResolver;

        public HeaderMenuViewComponent(IGravatarResolver gravatarResolver)
        {
            this.gravatarResolver = gravatarResolver;
        }

        public IViewComponentResult Invoke(ApplicationUser user)
        {
            var hasDismissedCookieBanner = Request.Cookies.ContainsKey("cookieWarningDismissed");
            var model = new HeaderMenuViewModel
                            {
                                UserName = user?.SkaterName,
                                UserProfileImage =
                                    string.IsNullOrWhiteSpace(user?.ExternalProfileImage)
                                        ? gravatarResolver.GetGravatarUrl(user?.Email)
                                        : user?.ExternalProfileImage,
                                IsLoggedIn = user != null,
                                ShowCookieBanner = user == null && !hasDismissedCookieBanner
            };

            return View("~/Views/Shared/Components/HeaderMenu/Default.cshtml", model);
        }
    }
}
