using System;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class StravaEvent
    {
        public Guid StravaEventId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public string StravaActivityId { get; set; }

        public bool Imported { get; set; }
    }
}
