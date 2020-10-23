using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Strava.Webhook.Deauthorise
{
    public class DeauthoriseStravaUserCommandResponse
    {
        public bool WasSuccessful { get; set; }

        public ApplicationUser UserDetails { get; set; }

        public List<StravaEvent> StravaEvents { get; set; }

        public List<SkateLogEntry> SkateLogs { get; set; }

        public string LogoUrl { get; set; }
    }
}
