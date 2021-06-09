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
    public class GetBestAverageSpeedLeadersQueryHandler : IRequestHandler<GetBestAverageSpeedLeadersQuery, List<SkaterStatisticsModel>>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public GetBestAverageSpeedLeadersQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<List<SkaterStatisticsModel>> Handle(GetBestAverageSpeedLeadersQuery request, CancellationToken cancellationToken)
        {
            var bestAverageSpeeds = await context.SkateLogEntries
                                                 .Where(x => x.AverageSpeedInMph > 0 && x.Duration >= 1800)
                                                 .GroupBy(x => x.ApplicationUserId)
                                                 .Select(x => new { ApplicationUserId = x.Key, BestAverageSpeed = x.Max(x => x.AverageSpeedInMph) })
                                                 .OrderByDescending(x => x.BestAverageSpeed)
                                                 .ToListAsync();

            var skaterIds = bestAverageSpeeds.Select(x => x.ApplicationUserId).ToList();

            var skaters = await context.Users.Where(x => skaterIds.Contains(x.Id)).ToListAsync();

            return (from bestAverageSpeed in bestAverageSpeeds
                    join skater in skaters on bestAverageSpeed.ApplicationUserId equals skater.Id
                    where skater.HasPaid
                    select new SkaterStatisticsModel
                    {
                        SkaterName = skater.SkaterName,
                        SkaterProfile = GetProfileImage(skater),
                        Statistic = $"{bestAverageSpeed.BestAverageSpeed:F2} MPH"
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
