using MediatR;

namespace AllInSkateChallenge.Features.Strava.Webhook.Deauthorise
{
    public class DeauthoriseStravaUserCommand : IRequest<DeauthoriseStravaUserCommandResponse>
    {
        public string StravaUserId { get; set; }
    }
}
