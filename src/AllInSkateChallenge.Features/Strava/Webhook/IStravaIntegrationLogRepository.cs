using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    public interface IStravaIntegrationLogRepository
    {
        Task Log(string queryString, WebHookEvent webHookEvent);

        Task<List<StravaIntegrationLog>> Get(int days);
    }
}
