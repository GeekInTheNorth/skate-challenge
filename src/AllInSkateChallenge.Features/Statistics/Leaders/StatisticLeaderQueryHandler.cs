using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class StatisticLeaderQuery : IRequest<IEnumerable<SkaterStatisticsModel>>
    {
        public StatisticType StatisticType { get; set; }
    }

    public class StatisticLeaderQueryHandler : IRequestHandler<StatisticLeaderQuery, IEnumerable<SkaterStatisticsModel>>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public StatisticLeaderQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<IEnumerable<SkaterStatisticsModel>> Handle(StatisticLeaderQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = request.StatisticType.Equals(StatisticType.BestAverageSpeed) ?
                context.SkateLogEntries.Where(x => x.ApplicationUser.HasPaid && x.AverageSpeedInMph > 0 && x.Duration >= 1800) : 
                context.SkateLogEntries.Where(x => x.ApplicationUser.HasPaid);

            var allSkaterSummaries = await baseQuery.GroupBy(x => x.ApplicationUserId)
                                                    .Select(x => new 
                                                    { 
                                                        ApplicationUserId = x.Key,
                                                        BestAverageSpeed = x.Max(x => x.AverageSpeedInMph),
                                                        BestTopSpeed = x.Max(x => x.TopSpeedInMph),
                                                        GreatestClimb = x.Max(x => x.ElevationGainInFeet),
                                                        SkybornSkater = x.Max(x => x.HighestElevationInFeet),
                                                        LongestJourney = x.Max(x => x.DistanceInMiles),
                                                        ShortestJourney = x.Min(x => x.DistanceInMiles),
                                                        LongestTotalDistance = x.Sum(x => x.DistanceInMiles),
                                                        MostJourneys = x.Count()
                                                    })
                                                    .ToListAsync();

            var skaterIds = allSkaterSummaries.Select(x => x.ApplicationUserId).ToList();
            var skaters = await context.Users.Where(x => skaterIds.Contains(x.Id)).ToListAsync();

            var skaterSummaries = new List<Tuple<string, string>>();
            switch (request.StatisticType)
            {
                case StatisticType.BestAverageSpeed:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.BestAverageSpeed > 0)
                                                               .OrderByDescending(x => x.BestAverageSpeed)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, $"{x.BestAverageSpeed:F2} MPH")));
                    break;
                case StatisticType.BestTopSpeed:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.BestTopSpeed > 0)
                                                               .OrderByDescending(x => x.BestTopSpeed)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, $"{x.BestTopSpeed:F2} MPH")));
                    break;
                case StatisticType.LongestDistance:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.LongestJourney > 0)
                                                               .OrderByDescending(x => x.LongestJourney)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, GetDistanceString(x.LongestJourney))));
                    break;
                case StatisticType.ShortestDistance:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.ShortestJourney > 0)
                                                               .OrderBy(x => x.ShortestJourney)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, GetDistanceString(x.ShortestJourney))));
                    break;
                case StatisticType.GreatestElevationGain:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.GreatestClimb > 0)
                                                               .OrderByDescending(x => x.GreatestClimb)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, $"{x.GreatestClimb:F2} Feet")));
                    break;
                case StatisticType.HighestElevation:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.SkybornSkater > 0)
                                                               .OrderByDescending(x => x.SkybornSkater)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, $"{x.SkybornSkater:F2} Feet")));
                    break;
                case StatisticType.LongestTotalJourney:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.LongestTotalDistance > 0)
                                                               .OrderByDescending(x => x.LongestTotalDistance)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, GetDistanceString(x.LongestTotalDistance))));
                    break;
                case StatisticType.MostJourneys:
                    skaterSummaries.AddRange(allSkaterSummaries.Where(x => x.MostJourneys > 0)
                                                               .OrderByDescending(x => x.MostJourneys)
                                                               .Select(x => new Tuple<string, string>(x.ApplicationUserId, $"{x.MostJourneys:F0} Journeys")));
                    break;
            }

            return (from skaterSummary in skaterSummaries
                    join skater in skaters on skaterSummary.Item1 equals skater.Id
                    where skater.HasPaid
                    select new SkaterStatisticsModel
                    {
                        SkaterName = skater.SkaterName,
                        SkaterProfile = GetProfileImage(skater),
                        Statistic = skaterSummary.Item2
                    }).ToList();
        }

        private string GetProfileImage(ApplicationUser skater)
        {
            return string.IsNullOrWhiteSpace(skater?.ExternalProfileImage)
                       ? gravatarResolver.GetGravatarUrl(skater?.Email)
                       : skater.ExternalProfileImage;
        }

        private string GetDistanceString(decimal distance)
        {
            return distance < 0.189394M ? $"{Conversion.MilesToFeet(distance):F2} feet" : $"{distance:F2} miles";
        }
    }
}
