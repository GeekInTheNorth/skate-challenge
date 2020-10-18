using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string SkaterName { get; set; }

        public string ExternalProfileImage { get; set; }

        public bool IsStravaAccount { get; set; }

        public bool AcceptProgressNotifications { get; set; }

        public bool HasPaid { get; set; }

        public List<SkateLogEntry> SkateLogEntries { get; set; }

        public List<StravaEvent> StravaEvents { get; set; }
    }
}
