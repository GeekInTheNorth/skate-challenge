using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQueryHandler : IRequestHandler<LatestUpdatesQuery, LatestUpdatesQueryResponse>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public LatestUpdatesQueryHandler(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<LatestUpdatesQueryResponse> Handle(LatestUpdatesQuery request, CancellationToken cancellationToken)
        {
            var userMilageEntries = from skateLogEntry in context.SkateLogEntries
                                    join user in context.Users on skateLogEntry.ApplicationUserId equals user.Id
                                    where user.HasPaid
                                    orderby skateLogEntry.Logged descending
                                    select new
                                    {
                                        Entry = skateLogEntry,
                                        User = user
                                    };

            var results = await userMilageEntries.Take(request.Limit).ToListAsync();

            return new LatestUpdatesQueryResponse
            {
                Entries = results.Select(x => new MileageEntryModel
                {
                    Logged = x.Entry.Logged,
                    Miles = x.Entry.DistanceInMiles,
                    GravatarUrl = gravatarResolver.GetGravatarUrl(x.User?.Email),
                    Skater = x.User.SkaterName ?? "Private Skater",
                }).ToList()
            };
        }
    }
}
