using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration
{
    public class StravaLogQueryResponse
    {
        public List<StravaIntegrationLog> Logs { get; set; }
    }
}
