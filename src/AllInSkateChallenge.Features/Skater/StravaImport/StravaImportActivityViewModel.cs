using System;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportActivityViewModel
    {
        public string ActivityId { get; set; }

        public string ActivityType { get; set; }

        public decimal Miles { get; set; }

        public decimal LowestElevation { get; set; }

        public decimal HighestElevation { get; set; }

        public decimal TotalElevation { get; set; }

        public decimal AverageSpeed { get; set; }

        public decimal TopSpeed { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Duration { get; set; }

        public bool IsImported { get; set; }

        public bool IsEligableActivity { get; set; }

        public string Name { get; set; }
    }
}
