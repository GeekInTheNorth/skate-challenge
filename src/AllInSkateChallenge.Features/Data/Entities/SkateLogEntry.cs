namespace AllInSkateChallenge.Features.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SkateLogEntry
    {
        public Guid SkateLogEntryId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal DistanceInMiles { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal ElevationGainInFeet { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal LowestElevationInFeet { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal HighestElevationInFeet { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal AverageSpeedInMph { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal TopSpeedInMph { get; set; }

        public DateTime Logged { get; set; }

        public int Duration { get; set; }

        public string StravaId { get; set; }

        public string Name { get; set; }

        public bool IsMultipleEntry { get; set; }
    }
}
