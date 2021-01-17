namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;

    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Static;

    public class SkaterTargetAnalysis
    {
        public ApplicationUser Skater { get; set; }

        public DateTime? DateReachedSaltaire { get; set; }

        public DateTime? DateReachedFoulridge { get; set; }

        public DateTime? DateReachedLiverpool { get; set; }

        public DateTime? DateReachedLeeds { get; set; }

        public int TotalSessions { get; set; }

        public decimal TotalMiles { get; set; }

        public Dictionary<SkateTarget, DateTime> CheckPointDates { get; set; }
    }
}