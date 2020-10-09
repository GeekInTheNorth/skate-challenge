using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Services.Strava
{
    public interface IStravaService
    {
        Task<StravaActivityListResponse> List(ApplicationUser applicationUser);
    }
}
