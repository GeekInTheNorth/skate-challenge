namespace AllInSkateChallenge.Features.Framework.Models
{
    using AllInSkateChallenge.Features.Data.Entities;

    public interface IPageViewModel
    {
        string PageTitle { get; }

        string DisplayPageTitle { get; }

        string IntroductoryText { get; }

        ApplicationUser CurrentUser { get; }
    
        bool IsStravaUser { get; }

        bool DisplayStravaNotification { get; }

        bool HasPaid { get; }

        bool IsNoIndexPage { get; }
    }
}
