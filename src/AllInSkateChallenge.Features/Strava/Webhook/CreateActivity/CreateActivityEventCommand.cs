using MediatR;

namespace AllInSkateChallenge.Features.Strava.Webhook.CreateActivity
{
    public class CreateActivityEventCommand : IRequest
    {
        public string StravaUserId { get; set; }

        public string ActivityId { get; set; }
    }
}
