namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;

    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Static;

    public class SkaterTargetAnalysis
    {
        public ApplicationUser Skater { get; set; }

        public int TotalSessions { get; set; }

        public decimal TotalMiles { get; set; }

        public Dictionary<SkateTarget, DateTime> CheckPointDates { get; set; }

        public DateTime? GetMileStoneDate(SkateTarget target)
        {
            if (CheckPointDates?.ContainsKey(target) ?? false)
            {
                return CheckPointDates[target];
            }

            return null;
        }
    }
}