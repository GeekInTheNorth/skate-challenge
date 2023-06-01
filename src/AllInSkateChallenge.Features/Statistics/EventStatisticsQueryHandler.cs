namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Data.Static;
    using AllInSkateChallenge.Features.Gravatar;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    public class EventStatisticsQueryHandler : IRequestHandler<EventStatisticsQuery, EventStatisticsQueryResponse>
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
                TotalMiles = allSessions.Sum(x => x.DistanceInMiles),
                TotalSkateSessions = allSessions.Count,
                MilesByStrava = allSessions.Where(x => !string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInMiles),
                MilesByManual = allSessions.Where(x => string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInMiles),
                JourneysByStrava = allSessions.Count(x => !string.IsNullOrWhiteSpace(x.StravaId)),
                JourneysByManual = allSessions.Count(x => string.IsNullOrWhiteSpace(x.StravaId)),
                SkateDistances = GetMilesPerWeek(allSessions, allWeeks).ToList(),
                SkateSessions = GetSessionsPerWeek(allSessions, allWeeks).ToList(),
                CheckPoints = GetCheckPointStatistics(skaterLogs).ToList(),
                ActivitiesByDay = GetActivitiesPerWeekDay(allSessions).ToList(),
                MilesByDay = GetMilesPerWeekDay(allSessions).ToList()
            };
        }

        private IEnumerable<StatisticsItemModel> GetSessionsPerWeek(IList<SkateLogEntry> allSessions, IList<Tuple<DateTime, DateTime>> allWeeks)
        {
            foreach (var week in allWeeks)
            {
                var miles = allSessions.Where(x => x.Logged.Date >= week.Item1 && x.Logged.Date <= week.Item2).Count();

                yield return new StatisticsItemModel { Date = week.Item1, Value = miles };
            }
        }

        private IEnumerable<StatisticsItemModel> GetMilesPerWeek(IList<SkateLogEntry> allSessions, IList<Tuple<DateTime, DateTime>> allWeeks)
        {
            foreach(var week in allWeeks)
            {
                var miles = allSessions.Where(x => x.Logged.Date >= week.Item1 && x.Logged.Date <= week.Item2).Sum(x => x.DistanceInMiles);

                yield return new StatisticsItemModel { Date = week.Item1, Value = miles };
            }
        }

        private List<StatisticsDayItemModel> GetActivitiesPerWeekDay(IList<SkateLogEntry> allSessions)
        {
            return allSessions.Select(x => new { x.Logged.DayOfWeek, x.DistanceInMiles })
                              .GroupBy(x => x.DayOfWeek)
                              .Select(x => new StatisticsDayItemModel { DayOfWeek = x.Key, Value = x.Count() })
                              .OrderBy(x => x.DayOfWeek)
                              .ToList();
        }

        private List<StatisticsDayItemModel> GetMilesPerWeekDay(IList<SkateLogEntry> allSessions)
        {
            return allSessions.Select(x => new { x.Logged.DayOfWeek, x.DistanceInMiles })
                              .GroupBy(x => x.DayOfWeek)
                              .Select(x => new StatisticsDayItemModel { DayOfWeek = x.Key, Value = x.Sum(x => x.DistanceInMiles) })
                              .OrderBy(x => x.DayOfWeek)
                              .ToList();
        }

        private List<Tuple<DateTime, DateTime>> GetAllWeeks(IList<SkateLogEntry> allSessions)
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

            var shortestDistance = allSessions.Where(x => !x.IsMultipleEntry).OrderBy(x => x.DistanceInMiles).First();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(shortestDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
                       {
                           SkaterName = skater?.GetDisplaySkaterName(),
                           Name = shortestDistance.Name,
                           Statistic = GetDisplayDistance(shortestDistance.DistanceInMiles),
                           SkaterProfile = GetProfileImage(skater)
                       };
        }

        private SkaterStatisticsModel GetLongestDistance(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
        {
            if (allSessions == null || !allSessions.Any())
            {
                return null;
            }

            var shortestDistance = allSessions.Where(x => !x.IsMultipleEntry).OrderByDescending(x => x.DistanceInMiles).First();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(shortestDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
                       {
                           SkaterName = skater?.GetDisplaySkaterName(),
                           Name = shortestDistance.Name,
                           Statistic = GetDisplayDistance(shortestDistance.DistanceInMiles),
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
                                                  .Select(x => new { ApplicationUserId = x.Key, TotalMiles = x.Sum(y => y.DistanceInMiles) })
                                                  .OrderByDescending(x => x.TotalMiles)
                                                  .First();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(longestTotalDistance.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
                       {
                           SkaterName = skater?.GetDisplaySkaterName(),
                           Statistic = GetDisplayDistance(longestTotalDistance.TotalMiles),
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

            var bestTopSpeed = allSessions.OrderByDescending(x => x.TopSpeedInMph).FirstOrDefault();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestTopSpeed.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
            {
                SkaterName = skater?.GetDisplaySkaterName(),
                Statistic = $"{bestTopSpeed.TopSpeedInMph:F2} MPH",
                SkaterProfile = GetProfileImage(skater)
            };
        }

        private SkaterStatisticsModel GetBestAverageSpeedSkater(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
        {
            if (allSessions == null || !allSessions.Any())
            {
                return null;
            }

            var bestAverageSpeed = allSessions.Where(x => x.Duration >= 1800).OrderByDescending(x => x.AverageSpeedInMph).FirstOrDefault();
            if (bestAverageSpeed == null)
            {
                return null;
            }

            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestAverageSpeed.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
            {
                SkaterName = skater?.GetDisplaySkaterName(),
                Statistic = $"{bestAverageSpeed.AverageSpeedInMph:F2} MPH",
                SkaterProfile = GetProfileImage(skater)
            };
        }

        private SkaterStatisticsModel GetGreatestClimber(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
        {
            if (allSessions == null || !allSessions.Any())
            {
                return null;
            }

            var bestElevationGain = allSessions.OrderByDescending(x => x.ElevationGainInFeet).FirstOrDefault();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(bestElevationGain.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
            {
                SkaterName = skater?.GetDisplaySkaterName(),
                Statistic = $"{bestElevationGain.ElevationGainInFeet:F2} Feet",
                SkaterProfile = GetProfileImage(skater)
            };
        }

        private SkaterStatisticsModel GetSkybornSkater(IList<SkateLogEntry> allSessions, IList<ApplicationUser> allSkaters)
        {
            if (allSessions == null || !allSessions.Any())
            {
                return null;
            }

            var highestAltitude = allSessions.OrderByDescending(x => x.HighestElevationInFeet).FirstOrDefault();
            var skater = allSkaters.FirstOrDefault(x => x.Id.Equals(highestAltitude.ApplicationUserId, StringComparison.CurrentCultureIgnoreCase));

            return new SkaterStatisticsModel
            {
                SkaterName = skater?.GetDisplaySkaterName(),
                Statistic = $"{highestAltitude.HighestElevationInFeet:F2} Feet",
                SkaterProfile = GetProfileImage(skater)
            };
        }

        private IEnumerable<CheckPointStatisticsModel> GetCheckPointStatistics(List<SkaterTargetAnalysis> skaterAnalyses)
        {
            var checkPoints = checkPointRepository.Get().Where(x => !x.SkateTarget.Equals(SkateTarget.CornExchange)).ToList();
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

        private string GetDisplayDistance(decimal distance)
        {
            return distance < 0.1M ? $"{(5280 * distance):F2} feet" : $"{distance:F2} miles";
        }
    }
}