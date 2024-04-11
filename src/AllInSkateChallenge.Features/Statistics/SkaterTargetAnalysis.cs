namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;

    using AllInSkateChallenge.Features.Data.Entities;

    public class SkaterTargetAnalysis
    {
        public ApplicationUser Skater { get; set; }

        public int TotalSessions { get; set; }

        public decimal TotalMiles { get; set; }

        public Dictionary<int, DateTime> CheckPointDates { get; set; }

        public DateTime? GetMileStoneDate(int target)
        {
            if (CheckPointDates?.ContainsKey(target) ?? false)
            {
                return CheckPointDates[target];
            }

            return null;
        }
    }
}