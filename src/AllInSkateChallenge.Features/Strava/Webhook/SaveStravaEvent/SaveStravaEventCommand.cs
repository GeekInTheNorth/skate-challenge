using System.Collections.Generic;

using MediatR;

namespace AllInSkateChallenge.Features.Strava.Webhook.SaveStravaEvent
{
    public class SaveStravaEventCommand : IRequest
    {
        public string StravaUserId { get; set; }

        public string ActivityId { get; set; }

        public Dictionary<string, string> Updates { get; set; }
    }
}
