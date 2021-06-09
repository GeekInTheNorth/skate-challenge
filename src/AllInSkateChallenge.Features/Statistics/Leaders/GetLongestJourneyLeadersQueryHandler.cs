using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class GetLongestJourneyLeadersQueryHandler : IRequestHandler<GetLongestJourneyLeadersQuery, List<SkaterStatisticsModel>>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public GetLongestJourneyLeadersQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<List<SkaterStatisticsModel>> Handle(GetLongestJourneyLeadersQuery request, CancellationToken cancellationToken)
        {
            var longestJourneys = await context.SkateLogEntries
                                               .Where(x => x.DistanceInMiles > 0)
                                               .GroupBy(x => x.ApplicationUserId)
                                               .Select(x => new { ApplicationUserId = x.Key, GreatestDistance = x.Max(x => x.DistanceInMiles) })
                                               .OrderByDescending(x => x.GreatestDistance)
                                               .ToListAsync();

            var skaterIds = longestJourneys.Select(x => x.ApplicationUserId).ToList();

            var skaters = await context.Users.Where(x => skaterIds.Contains(x.Id)).ToListAsync();

            return (from longestJourney in longestJourneys
                    join skater in skaters on longestJourney.ApplicationUserId equals skater.Id
                    where skater.HasPaid
                    select new SkaterStatisticsModel
                    {
                        SkaterName = skater.SkaterName,
                        SkaterProfile = GetProfileImage(skater),
                        Statistic = $"{longestJourney.GreatestDistance:F2} Miles"
                    }).ToList();
        }

        private string GetProfileImage(ApplicationUser skater)
        {
            return string.IsNullOrWhiteSpace(skater?.ExternalProfileImage)
                       ? gravatarResolver.GetGravatarUrl(skater?.Email)
                       : skater.ExternalProfileImage;
        }
    }
}
