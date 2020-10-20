namespace AllInSkateChallenge.Features.Framework.Models
{
    public class PageViewModel<T> : IPageViewModel
        where T : class
    {
        public T Content { get; set; }

        public string PageTitle { get; set; }

        public string DisplayPageTitle { get; set; }

        public string IntroductoryText { get; set; }

        public string DisplayUserName { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }

        public bool IsStravaUser { get; set; }

        public bool HasStravaImports { get; set; }

        public bool DisplayStravaNotification { get; set; }
        
        public bool HasPaid { get; set; }
    }
}
