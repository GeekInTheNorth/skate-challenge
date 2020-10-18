using MediatR;

namespace AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration
{
    public class LogStravaIntegrationCommand : IRequest
    {
        public WebHookEvent Event { get; set; }
    }
}
