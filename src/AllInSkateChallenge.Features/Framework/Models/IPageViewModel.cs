namespace AllInSkateChallenge.Features.Framework.Models
{
    public interface IPageViewModel
    {
        public string PageTitle { get; set; }

        public string DisplayPageTitle { get; set; }

        public string IntroductoryText { get; set; }

        string DisplayUserName { get; }

        bool IsAdmin { get; }

        bool IsLoggedIn { get; }

        bool IsStravaUser { get; }

        bool HasStravaImports { get; }

        public bool DisplayStravaNotification { get; set; }
    }
}
