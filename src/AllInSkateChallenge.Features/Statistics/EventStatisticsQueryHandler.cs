namespace AllInSkateChallenge.Features.Statistics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

public sealed class EventStatisticsQueryHandler : IRequestHandler<EventStatisticsQuery, EventStatisticsQueryResponse>
{
    private readonly ApplicationDbContext context;

    private readonly IGravatarResolver gravatarResolver;

    private readonly ISkaterTargetAnalyser skaterTargetAnalyser;

    private readonly ICheckPointRepository checkPointRepository;

    public EventStatisticsQueryHandler(
        ApplicationDbContext context,
        IGravatarResolver gravatarResolver,
        ISkaterTargetAnalyser skaterTargetAnalyser, 
        ICheckPointRepository checkPointRepository)
    {
        this.context = context;
        this.gravatarResolver = gravatarResolver;
        this.skaterTargetAnalyser = skaterTargetAnalyser;
        this.checkPointRepository = checkPointRepository;
    }

    public async Task<EventStatisticsQueryResponse> Handle(EventStatisticsQuery request, CancellationToken cancellationToken)
    {
        var allSessionsQuery = context.SkateLogEntries.Where(x => x.ApplicationUser.HasPaid);
        var doingPeriodRange = request.DateFrom.HasValue && request.DateTo.HasValue;
        if (doingPeriodRange)
        {
            allSessionsQuery = allSessionsQuery.Where(x => x.Logged >= request.DateFrom.Value && x.Logged <= request.DateTo.Value);
        }

        var allSessions = await allSessionsQuery.ToListAsync(cancellationToken);
        if (!allSessions.Any())
        {
            return new EventStatisticsQueryResponse();
        }

        var allSkaters = await context.Users.Where(x => x.HasPaid).ToListAsync(cancellationToken);
        var skaterLogs = allSkaters.Select(x => skaterTargetAnalyser.Analyse(x, allSessions)).Where(x => x.TotalSessions > 0).ToList();
        var allWeeks = GetAllWeeks(allSessions);

        if (doingPeriodRange)
        {
            allWeeks = allWeeks.Where(x => x.Item1 <= request.DateTo.Value).ToList();
        }

        return new EventStatisticsQueryResponse
        {
            ShortestSingleDistance = GetShortestDistance(allSessions, allSkaters),
            LongestSingleDistance = GetLongestDistance(allSessions, allSkaters),
            LongestTotalDistance = GetLongestTotalDistance(allSessions, allSkaters),
            MostJourneys = GetHighestNumberOfJourneys(allSessions, allSkaters),
            BestTopSpeed = GetFastestSkater(allSessions, allSkaters),
            BestAverageSpeed = GetBestAverageSpeedSkater(allSessions, allSkaters),
            GreatestClimb = GetGreatestClimber(allSessions, allSkaters),
            SkybornSkater = GetSkybornSkater(allSessions, allSkaters),
            TotalKilometres = allSessions.Sum(x => x.DistanceInKilometres),
            TotalSkateSessions = allSessions.Count,
            KilometresByStrava = allSessions.Where(x => !string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInKilometres),
            KilometresByManual = allSessions.Where(x => string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInKilometres),
            JourneysByStrava = allSessions.Count(x => !string.IsNullOrWhiteSpace(x.StravaId)),
            JourneysByManual = allSessions.Count(x => string.IsNullOrWhiteSpace(x.StravaId)),
            SkateDistances = GetKilometresPerWeek(allSessions, allWeeks).ToList(),
            SkateSessions = GetSessionsPerWeek(allSessions, allWeeks).ToList(),
            CheckPoints = GetCheckPointStatistics(skaterLogs).ToList(),
            ActivitiesByDay = GetActivitiesPerWeekDay(allSessions).ToList(),
            KilometresByDay = GetDistancePerWeekDay(allSessions).ToList()
        };
    }

    private static IEnumerable<StatisticsItemModel> GetSessionsPerWeek(IList<SkateLogEntry> allSessions, IList<Tuple<DateTime, DateTime>> allWeeks)
    {
        foreach (var week in allWeeks)
        {
            var sessions = allSessions.Where(x => x.Logged.Date >= week.Item1 && x.Logged.Date <= week.Item2).Count();

            yield return new StatisticsItemModel { Date = week.Item1, Value = sessions };
        }
    }

    private static IEnumerable<StatisticsItemModel> GetKilometresPerWeek(IList<SkateLogEntry> allSessions, IList<Tuple<DateTime, DateTime>> allWeeks)
    {
        foreach(var week in allWeeks)
        {
            var kilometres = allSessions.Where(x => x.Logged.Date >= week.Item1 && x.Logged.Date <= week.Item2).Sum(x => x.DistanceInKilometres);

            yield return new StatisticsItemModel { Date = week.Item1, Value = kilometres };
        }
    }

    private static List<StatisticsDayItemModel> GetActivitiesPerWeekDay(IList<SkateLogEntry> allSessions)
    {
        return allSessions.Select(x => new { x.Logged.DayOfWeek, x.DistanceInKilometres })
                          .GroupBy(x => x.DayOfWeek)
                          .Select(x => new StatisticsDayItemModel { DayOfWeek = x.Key, Value = x.Count() })
                          .OrderBy(x => x.DayOfWeek)
                          .ToList();
    }

    private static List<StatisticsDayItemModel> GetDistancePerWeekDay(IList<SkateLogEntry> allSessions)
    {
        return allSessions.Select(x => new { x.Logged.DayOfWeek, x.DistanceInKilometres })
                          .GroupBy(x => x.DayOfWeek)
                          .Select(x => new StatisticsDayItemModel { DayOfWeek = x.Key, Value = x.Sum(x => x.DistanceInKilometres) })
                          .OrderBy(x => x.DayOfWeek)
                          .ToList();
    }

    private static List<Tuple<DateTime, DateTime>> GetAllWeeks(IList<SkateLogEntry> allSessions)
    {
        var workingDate = allSessions.Min(x => x.Logged).Date;
        var today = DateTime.Today;
        var weeks = new List<Tuple<DateTime, DateTime>>();

        while (workingDate < today)
        {
            weeks.Add(new Tuple<DateTime, DateTime>(workingDate, workingDate.AddDays(6)));
            workingDate = workingDate.AddDays(7);
        }

        return weeks;
    }

    private SkaterStatisticsModel GetShortestDistance(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var shortestDistance = allSessions.Where(x => !x.IsMultipleEntry).OrderBy(x => x.DistanceInKilometres).First();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(shortestDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
                   {
                       SkaterName = skater?.GetDisplaySkaterName(),
                       Name = shortestDistance.Name,
                       Statistic = GetDisplayDistance(shortestDistance.DistanceInKilometres),
                       SkaterProfile = GetProfileImage(skater)
                   };
    }

    private SkaterStatisticsModel GetLongestDistance(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var shortestDistance = allSessions.Where(x => !x.IsMultipleEntry).OrderByDescending(x => x.DistanceInKilometres).First();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(shortestDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
                   {
                       SkaterName = skater?.GetDisplaySkaterName(),
                       Name = shortestDistance.Name,
                       Statistic = GetDisplayDistance(shortestDistance.DistanceInKilometres),
                       SkaterProfile = GetProfileImage(skater)
                   };
    }

    private SkaterStatisticsModel GetLongestTotalDistance(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var longestTotalDistance = allSessions.GroupBy(x => x.ApplicationUserId)
                                              .Select(x => new { ApplicationUserId = x.Key, TotalKilometres = x.Sum(y => y.DistanceInKilometres) })
                                              .OrderByDescending(x => x.TotalKilometres)
                                              .First();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(longestTotalDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
                   {
                       SkaterName = skater?.GetDisplaySkaterName(),
                       Statistic = GetDisplayDistance(longestTotalDistance.TotalKilometres),
                       SkaterProfile = GetProfileImage(skater)
                   };
    }

    private SkaterStatisticsModel GetHighestNumberOfJourneys(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var mostJourneys = allSessions.GroupBy(x => x.ApplicationUserId)
                                      .Select(x => new { ApplicationUserId = x.Key, Journeys = x.Count() })
                                      .OrderByDescending(x => x.Journeys)
                                      .First();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(mostJourneys.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
        {
            SkaterName = skater?.GetDisplaySkaterName(),
            Statistic = $"{mostJourneys.Journeys:F0} Journeys",
            SkaterProfile = GetProfileImage(skater)
        };
    }

    private SkaterStatisticsModel GetFastestSkater(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var bestTopSpeed = allSessions.OrderByDescending(x => x.TopSpeedInKph).FirstOrDefault();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestTopSpeed.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
        {
            SkaterName = skater?.GetDisplaySkaterName(),
            Statistic = $"{bestTopSpeed.TopSpeedInKph:F2} KPH",
            SkaterProfile = GetProfileImage(skater)
        };
    }

    private SkaterStatisticsModel GetBestAverageSpeedSkater(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var bestAverageSpeed = allSessions.Where(x => x.Duration >= 1800).OrderByDescending(x => x.AverageSpeedInKph).FirstOrDefault();
        if (bestAverageSpeed == null)
        {
            return null;
        }

        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestAverageSpeed.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
        {
            SkaterName = skater?.GetDisplaySkaterName(),
            Statistic = $"{bestAverageSpeed.AverageSpeedInKph:F2} KPH",
            SkaterProfile = GetProfileImage(skater)
        };
    }

    private SkaterStatisticsModel GetGreatestClimber(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var bestElevationGain = allSessions.OrderByDescending(x => x.ElevationGainInMetres).FirstOrDefault();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestElevationGain.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
        {
            SkaterName = skater?.GetDisplaySkaterName(),
            Statistic = $"{bestElevationGain.ElevationGainInMetres:F2} Metres",
            SkaterProfile = GetProfileImage(skater)
        };
    }

    private SkaterStatisticsModel GetSkybornSkater(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
    {
        if (allSessions == null || !allSessions.Any())
        {
            return null;
        }

        var highestAltitude = allSessions.OrderByDescending(x => x.HighestElevationInMetres).FirstOrDefault();
        var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(highestAltitude.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

        return new SkaterStatisticsModel
        {
            SkaterName = skater?.GetDisplaySkaterName(),
            Statistic = $"{highestAltitude.HighestElevationInMetres:F2} Metres",
            SkaterProfile = GetProfileImage(skater)
        };
    }

    private IEnumerable<CheckPointStatisticsModel> GetCheckPointStatistics(List<SkaterTargetAnalysis> skaterAnalyses)
    {
        var checkPoints = checkPointRepository.Get().Skip(1).ToList();
        foreach(var checkPoint in checkPoints)
        {
            var firstSkater = skaterAnalyses.Where(x => x.CheckPointDates.ContainsKey(checkPoint.SkateTarget))
                                            .OrderBy(x => x.GetMileStoneDate(checkPoint.SkateTarget))
                                            .Select(x => x.Skater)
                                            .FirstOrDefault();
            var lastSkater = skaterAnalyses.Where(x => x.CheckPointDates.ContainsKey(checkPoint.SkateTarget))
                                           .OrderByDescending(x => x.GetMileStoneDate(checkPoint.SkateTarget))
                                           .Select(x => x.Skater)
                                           .FirstOrDefault();

            yield return new CheckPointStatisticsModel
            {
                Target = checkPoint.SkateTarget,
                CheckPointName = checkPoint.Title,
                FirstSkaterName = firstSkater?.GetDisplaySkaterName(),
                FirstSkaterProfile = firstSkater != null ? GetProfileImage(firstSkater) : null,
                LatestSkaterName = lastSkater?.GetDisplaySkaterName(),
                LatestSkaterProfile = lastSkater != null ? GetProfileImage(lastSkater) : null
            };
        }
    }

    private string GetProfileImage(ApplicationUser skater)
    {
        return string.IsNullOrWhiteSpace(skater?.ExternalProfileImage)
                   ? gravatarResolver.GetGravatarUrl(skater?.Email)
                   : skater.ExternalProfileImage;
    }

    private static string GetDisplayDistance(decimal distance)
    {
        return distance < 0.1M ? $"{Conversion.KilometresToMetres(distance):F2} metres" : $"{distance:F2} kilometres";
    }
}