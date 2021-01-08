namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Gravatar;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    public class EventStatisticsQueryHandler : IRequestHandler<EventStatisticsQuery, EventStatisticsQueryResponse>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public EventStatisticsQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<EventStatisticsQueryResponse> Handle(EventStatisticsQuery request, CancellationToken cancellationToken)
        {
            var allSessions = await context.SkateLogEntries.Where(x => x.ApplicationUser.HasPaid).ToListAsync(cancellationToken);
            var allSkaters = await context.Users.Where(x => x.HasPaid).ToListAsync(cancellationToken);
            var allDates = GetAllDates(allSessions);

            return new EventStatisticsQueryResponse
                       {
                           ShortestSingleDistance = GetShortestDistance(allSessions, allSkaters),
                           LongestSingleDistance = GetLongestDistance(allSessions, allSkaters),
                           LongestTotalDistance = GetLongestTotalDistance(allSessions, allSkaters),
                           TotalMiles = allSessions.Sum(x => x.DistanceInMiles),
                           TotalSkateSessions = allSessions.Count,
                           MilesByStrava = allSessions.Where(x => !string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInMiles),
                           MilesByManual = allSessions.Where(x => string.IsNullOrWhiteSpace(x.StravaId)).Sum(x => x.DistanceInMiles),
                           JourneysByStrava = allSessions.Count(x => !string.IsNullOrWhiteSpace(x.StravaId)),
                           JourneysByManual = allSessions.Count(x => string.IsNullOrWhiteSpace(x.StravaId)),
                           SkateDistances = GetMilesPerDay(allSessions, allDates),
                           SkateSessions = GetSessionsPerDay(allSessions, allDates)
                       };
        }

        private List<StatisticsItemModel> GetSessionsPerDay(IList<SkateLogEntry> allSessions, IList<DateTime> allDates)
        {
            var sessionsByDate = allSessions.Where(x => !x.IsMultipleEntry)
                                            .GroupBy(x => x.Logged.Date)
                                            .Select(x => new { Date = x.Key, Value = x.Count() })
                                            .OrderBy(x => x.Date)
                                            .ToList();

            return (from dates in allDates
                    join sessions in sessionsByDate on dates equals sessions.Date
                    orderby dates
                    select new StatisticsItemModel { Date = dates, Value = sessions?.Value ?? 0 }).ToList();
        }

        private List<StatisticsItemModel> GetMilesPerDay(IList<SkateLogEntry> allSessions, IList<DateTime> allDates)
        {
            var milesByDate = allSessions.Where(x => !x.IsMultipleEntry)
                                         .GroupBy(x => x.Logged.Date)
                                         .Select(x => new { Date = x.Key, Value = x.Sum(y => y.DistanceInMiles) })
                                         .OrderBy(x => x.Date)
                                         .ToList();

            return (from dates in allDates
                    join miles in milesByDate on dates equals miles.Date
                    orderby dates
                    select new StatisticsItemModel { Date = dates, Value = miles?.Value ?? 0 }).ToList();
        }

        private List<DateTime> GetAllDates(IList<SkateLogEntry> allSessions)
        {
            var earliestDate = allSessions.Min(x => x.Logged).Date;
            var today = DateTime.Today;
            var days = (int)(today - earliestDate).TotalDays;

            return Enumerable.Range(0, days).Select(x => earliestDate.AddDays(x)).ToList();
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
                           DisplayDistance = GetDisplayDistance(shortestDistance.DistanceInMiles),
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
                           DisplayDistance = GetDisplayDistance(shortestDistance.DistanceInMiles),
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
                           DisplayDistance = GetDisplayDistance(longestTotalDistance.TotalMiles),
                           SkaterProfile = GetProfileImage(skater)
                       };
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