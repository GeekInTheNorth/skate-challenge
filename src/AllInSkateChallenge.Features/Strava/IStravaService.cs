using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Strava
{
    public interface IStravaService
    {
        Task<StravaActivityListResponse> List(ApplicationUser applicationUser);
    }
}
