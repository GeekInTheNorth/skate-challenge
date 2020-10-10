using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    public interface IStravaIntegrationLogRepository
    {
        Task Log(string queryString, WebHookEvent webHookEvent);
    }
}
