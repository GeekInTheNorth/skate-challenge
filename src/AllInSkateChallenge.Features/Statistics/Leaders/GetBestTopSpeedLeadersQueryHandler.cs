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
    public class GetBestTopSpeedLeadersQueryHandler : IRequestHandler<GetBestTopSpeedLeadersQuery, List<SkaterStatisticsModel>>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public GetBestTopSpeedLeadersQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<List<SkaterStatisticsModel>> Handle(GetBestTopSpeedLeadersQuery request, CancellationToken cancellationToken)
        {
            var bestTopSpeeds = await context.SkateLogEntries
                                             .Where(x => x.TopSpeedInMph > 0)
                                             .GroupBy(x => x.ApplicationUserId)
                                             .Select(x => new { ApplicationUserId = x.Key, BestTopSpeed = x.Max(x => x.TopSpeedInMph) })
                                             .OrderByDescending(x => x.BestTopSpeed)
                                             .ToListAsync();

            var skaterIds = bestTopSpeeds.Select(x => x.ApplicationUserId).ToList();

            var skaters = await context.Users.Where(x => skaterIds.Contains(x.Id)).ToListAsync();

            return (from bestTopSpeed in bestTopSpeeds
                    join skater in skaters on bestTopSpeed.ApplicationUserId equals skater.Id
                    where skater.HasPaid
                    select new SkaterStatisticsModel
                    {
                        SkaterName = skater.SkaterName,
                        SkaterProfile = GetProfileImage(skater),
                        Statistic = $"{bestTopSpeed.BestTopSpeed:F2} MPH"
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
