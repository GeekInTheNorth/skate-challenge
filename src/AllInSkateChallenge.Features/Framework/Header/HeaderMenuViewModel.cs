namespace AllInSkateChallenge.Features.Framework.Header
{
    public class HeaderMenuViewModel
    {
        public string UserName { get; set; }

        public string UserProfileImage { get; set; }

        public bool IsLoggedIn { get; set; }

        public bool ShowCookieBanner { get; set; }

        public bool IsRegistrationOver { get; set; }
    }
}