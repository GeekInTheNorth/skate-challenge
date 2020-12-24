namespace AllInSkateChallenge.Features.Framework.Models
{
    public interface IPageViewModel
    {
        string PageTitle { get; }

        string DisplayPageTitle { get; }

        string IntroductoryText { get; }

        bool IsAdmin { get; }

        bool IsLoggedIn { get; }

        bool IsStravaUser { get; }

        bool HasStravaImports { get; }

        bool DisplayStravaNotification { get; }

        bool HasPaid { get; }

        bool ShowCookieBanner { get; }

        bool IsNoIndexPage { get; }
    }
}
