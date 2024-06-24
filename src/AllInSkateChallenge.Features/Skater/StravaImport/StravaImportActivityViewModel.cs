using System;
using System.Linq;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportActivityViewModel
    {
        private static readonly string[] EligableActivities = { "IceSkate", "InlineSkate", "Skateboard" };

        private static readonly DateTime EarliestDate = new DateTime(2024, 6, 21, 0, 0, 0);

        public string ActivityId { get; set; }

        public string ActivityType { get; set; }

        public string DisplayActivityType { get; set; }

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

        public string Name { get; set; }

        public bool IsEligableActivity => GetIsEligableActivity();

        private bool GetIsEligableActivity()
        {
            return EligableActivities.Any(x => string.Equals(x, ActivityType, StringComparison.OrdinalIgnoreCase))
                && EarliestDate <= this.StartDate;
        }
    }
}
