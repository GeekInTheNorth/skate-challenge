namespace AllInSkateChallenge.Features.Strava.User;

using AllInSkateChallenge.Features.Data.Entities;

public class StavaDetails
{
    public ApplicationUser User { get; set; }

    public bool IsStravaAuthenticated { get; set; }

    public string StravaId { get; set; }
}