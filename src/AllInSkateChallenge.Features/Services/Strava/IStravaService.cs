using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Services.Strava
{
    public interface IStravaService
    {
        Task<List<StravaActivity>> List(ApplicationUser applicationUser);
    }
}
