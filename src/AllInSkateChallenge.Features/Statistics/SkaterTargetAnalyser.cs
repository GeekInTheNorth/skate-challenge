namespace AllInSkateChallenge.Features.Statistics;

using System;
using System.Collections.Generic;
using System.Linq;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;

public class SkaterTargetAnalyser : ISkaterTargetAnalyser
{
    private readonly ICheckPointRepository checkPointRepository;

    public SkaterTargetAnalyser(ICheckPointRepository checkPointRepository)
    {
        this.checkPointRepository = checkPointRepository;
    }

    public SkaterTargetAnalysis Analyse(ApplicationUser skater, List<SkateLogEntry> allSessions)
    {
        var orderedLog = allSessions.Where(x => x.ApplicationUserId.Equals(skater.Id, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.Logged);
        var totalMiles = 0M;
        var totalSessions = 0;
        var checkPoints = checkPointRepository.Get().Skip(1).ToList();
        var statistics = new SkaterTargetAnalysis
        {
            Skater = skater,
            CheckPointDates = new Dictionary<int, DateTime>()
        };
        
        foreach(var session in orderedLog)
        {
            totalSessions++;
            totalMiles += session.DistanceInMiles;

            foreach(var checkPoint in checkPoints)
            {
                if (totalMiles >= checkPoint.DistanceInMiles && !statistics.CheckPointDates.ContainsKey(checkPoint.SkateTarget))
                {
                    statistics.CheckPointDates.Add(checkPoint.SkateTarget, session.Logged);
                }
            }
        }

        statistics.TotalMiles = totalMiles;
        statistics.TotalSessions = totalSessions;

        return statistics;
    }
}