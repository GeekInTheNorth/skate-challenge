using System;
using System.ComponentModel.DataAnnotations;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public partial class SkaterLogViewModel : INewSkaterLogEntry
    {
        public string JourneyName { get; set; }

        [Required]
        public DistanceUnit DistanceUnit { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Distance { get; set; }

        public DateTime? DateSkated { get; set; }

        public VelocityUnit VelocityUnit { get; set; }

        [Range(0, 100)]
        public decimal AverageSpeed { get; set;  }

        [Range(0, 100)]
        public decimal TopSpeed { get; set; }

        public DistanceUnit ElevationUnit { get; set; }

        [Range(0, 10000)]
        public decimal LowestElevation { get; set; }

        [Range(0, 10000)]
        public decimal HighestElevation { get; set; }

        [Range(0, 10000)]
        public decimal ElevationGain { get; set; }
    }
}
