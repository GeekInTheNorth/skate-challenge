namespace AllInSkateChallenge.Features.Framework.Models
{
    using AllInSkateChallenge.Features.Data.Entities;

    public class PageViewModel<T> : IPageViewModel
        where T : class
    {
        public T Content { get; set; }

        public string PageTitle { get; set; }

        public string DisplayPageTitle { get; set; }

        public string IntroductoryText { get; set; }

        public ApplicationUser CurrentUser { get; set; }

        public bool IsLoggedIn => CurrentUser != null;

        public bool IsStravaUser => CurrentUser?.IsStravaAccount ?? false;

        public bool HasPaid => CurrentUser?.HasPaid ?? false;

        public bool DisplayStravaNotification { get; set; }

        public bool ShowCookieBanner { get; set; }

        public bool IsNoIndexPage { get; set; }
    }
}
