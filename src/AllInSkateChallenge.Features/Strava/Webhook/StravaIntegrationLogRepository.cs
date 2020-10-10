using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    public class StravaIntegrationLogRepository : IStravaIntegrationLogRepository
    {
        private readonly ApplicationDbContext context;

        public StravaIntegrationLogRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Log(string queryString, WebHookEvent webHookEvent)
        {
            var serializedEvent = JsonConvert.SerializeObject(webHookEvent);
            var log = new StravaIntegrationLog
            {
                Recieved = DateTime.Now,
                QueryString = queryString,
                Body = serializedEvent
            };

            context.StravaIntegrationLogs.Add(log);
            await context.SaveChangesAsync();

        }
    }
}
