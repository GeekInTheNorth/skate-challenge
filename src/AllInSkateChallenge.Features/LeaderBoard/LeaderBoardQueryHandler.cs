using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQueryHandler : IRequestHandler<LeaderBoardQuery, LeaderBoardQueryResponse>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public LeaderBoardQueryHandler(
            ApplicationDbContext context,
            IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public async Task<LeaderBoardQueryResponse> Handle(LeaderBoardQuery request, CancellationToken cancellationToken)
        {
            var distanceTotals = from entries in context.SkateLogEntries
                                 group entries by entries.ApplicationUserId into userEntries
                                 select new
                                 {
                                     UserId = userEntries.Key,
                                     TotalMiles = userEntries.Sum(x => x.DistanceInMiles)
                                 };

            var userMilageEntries = from distanceTotal in distanceTotals
                                    join user in context.Users on distanceTotal.UserId equals user.Id
                                    where user.HasPaid == true
                                    orderby distanceTotal.TotalMiles descending
                                    select new
                                    {
                                        Distance = distanceTotal,
                                        User = user
                                    };

            if (request.Limit.HasValue)
            {
                userMilageEntries = userMilageEntries.Take(request.Limit.Value);
            }

            var results = await userMilageEntries.ToListAsync();

            return new LeaderBoardQueryResponse
            {
                Entries = results.Select((x, i) => new LeaderBoardEntryModel
                {
                    Place = i + 1,
                    GravatarUrl = gravatarResolver.GetGravatarUrl(x.User?.Email),
                    Name = x.User.SkaterName ?? "Private Skater",
                    TotalMiles = x.Distance.TotalMiles
                }).ToList()
            };
        }
    }
}
