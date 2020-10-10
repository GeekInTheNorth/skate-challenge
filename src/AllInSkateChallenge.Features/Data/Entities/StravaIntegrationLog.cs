using System;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class StravaIntegrationLog
    {
        public Guid StravaIntegrationLogId { get; set; }

        public DateTime Recieved { get; set; }

        public string QueryString { get; set; }

        public string Body { get; set; }
    }
}
