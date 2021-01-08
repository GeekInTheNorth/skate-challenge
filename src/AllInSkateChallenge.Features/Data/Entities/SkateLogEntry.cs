﻿namespace AllInSkateChallenge.Features.Data.Entities
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

        public DateTime Logged { get; set; }

        public string StravaId { get; set; }

        public string Name { get; set; }

        public bool IsMultipleEntry { get; set; }
    }
}
