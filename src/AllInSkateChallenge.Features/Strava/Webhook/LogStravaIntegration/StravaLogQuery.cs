using MediatR;

namespace AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration
{
    public class StravaLogQuery : IRequest<StravaLogQueryResponse>
    {
        public int NumberOfDays { get; set; }
    }
}
