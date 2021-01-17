namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Static;

    public class SkaterTargetAnalyser : ISkaterTargetAnalyser
    {
        private readonly ICheckPointRepository checkPointRepository;

        public SkaterTargetAnalyser(ICheckPointRepository checkPointRepository)
        {
            this.checkPointRepository = checkPointRepository;
        }

        public SkaterTargetAnalysis Analyse(ApplicationUser skater, List<SkateLogEntry> allSessions)
        {
            var orderedLog = allSessions.Where(x => x.ApplicationUserId.Equals(skater.Id, System.StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.Logged);
            var totalMiles = 0M;
            var totalSessions = 0;
            var checkPoints = checkPointRepository.Get().Where(x => !x.SkateTarget.Equals(SkateTarget.None)).ToList();
            var statistics = new SkaterTargetAnalysis
            {
                Skater = skater,
                CheckPointDates = new Dictionary<SkateTarget, DateTime>()
            };
            
            foreach(var session in orderedLog)
            {
                totalSessions++;
                totalMiles += session.DistanceInMiles;

                foreach(var checkPoint in checkPoints)
                {
                    if (totalMiles >= checkPoint.Distance && !statistics.CheckPointDates.ContainsKey(checkPoint.SkateTarget))
                    {
                        statistics.CheckPointDates.Add(checkPoint.SkateTarget, session.Logged);
                    }
                }

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