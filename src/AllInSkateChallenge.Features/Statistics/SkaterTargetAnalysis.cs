namespace AllInSkateChallenge.Features.Statistics
{
    using System;

    using AllInSkateChallenge.Features.Data.Entities;

    public class SkaterTargetAnalysis
    {
        public ApplicationUser Skater { get; set; }

        public DateTime? DateReachedSaltaire { get; set; }

        public DateTime? DateReachedFoulridge { get; set; }

        public DateTime? DateReachedLiverpool { get; set; }

        public DateTime? DateReachedLeeds { get; set; }

        public int TotalSessions { get; set; }

        public decimal TotalMiles { get; set; }
    }
}