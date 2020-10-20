using System;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportActivityViewModel
    {
        public string ActivityId { get; set; }

        public string ActivityType { get; set; }

        public decimal Miles { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsImported { get; set; }

        public bool IsEligableActivity { get; set; }
    }
}
