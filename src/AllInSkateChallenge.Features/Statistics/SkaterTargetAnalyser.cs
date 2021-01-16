namespace AllInSkateChallenge.Features.Statistics
{
    using System.Collections.Generic;
    using System.Linq;

    using AllInSkateChallenge.Features.Data.Entities;

    public class SkaterTargetAnalyser : ISkaterTargetAnalyser
    {
        public SkaterTargetAnalysis Analyse(ApplicationUser skater, List<SkateLogEntry> allSessions)
        {
            var orderedLog = allSessions.Where(x => x.ApplicationUserId.Equals(skater.Id, System.StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.Logged);
            var totalMiles = 0M;
            var totalSessions = 0;
            var statistics = new SkaterTargetAnalysis
            {
                Skater = skater
            };
            
            foreach(var session in orderedLog)
            {
                totalSessions++;
                totalMiles += session.DistanceInMiles;

                if (totalMiles >= 13)
                {
                    statistics.DateReachedSaltaire ??= session.Logged;
                }

                if (totalMiles >= 47)
                {
                    statistics.DateReachedFoulridge ??= session.Logged;
                }

                if (totalMiles >= 127.5M)
                {
                    statistics.DateReachedLiverpool ??= session.Logged;
                }

                if (totalMiles >= 255)
                {
                    statistics.DateReachedLeeds ??= session.Logged;
                }
            }

            statistics.TotalMiles = totalMiles;
            statistics.TotalSessions = totalSessions;

            return statistics;
        }
    }
}