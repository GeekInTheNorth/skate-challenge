namespace AllInSkateChallenge.Features.Framework.Models
{
    public class UserStateQueryResponse
    {
        public bool IsLoggedIn { get; set; }

        public bool IsStravaUser { get; set; }

        public bool IsAdmin { get; set; }

        public bool HasPaid { get; set; }

        public bool HasStravaImports { get; set; }

        public bool HasDismissedCookieBanner { get; set; }

        public string SkaterName { get; set; }

        public string ProfileImage { get; set; }
    }
}
