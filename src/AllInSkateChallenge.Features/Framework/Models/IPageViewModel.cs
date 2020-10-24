namespace AllInSkateChallenge.Features.Framework.Models
{
    public interface IPageViewModel
    {
        public string PageTitle { get; }

        public string DisplayPageTitle { get; }

        public string IntroductoryText { get; }

        string DisplayUserName { get; }

        bool IsAdmin { get; }

        bool IsLoggedIn { get; }

        bool IsStravaUser { get; }

        bool HasStravaImports { get; }

        public bool DisplayStravaNotification { get; }

        bool HasPaid { get; }

        bool ShowCookieBanner { get; }
    }
}
