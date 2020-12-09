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

            var userMileageEntries = from distanceTotal in distanceTotals
                                     join user in context.Users on distanceTotal.UserId equals user.Id
                                     where user.HasPaid && user.Target == request.Target
                                     orderby distanceTotal.TotalMiles descending
                                     select new { Distance = distanceTotal, User = user };

            if (request.PageSize.HasValue)
            {
                userMileageEntries = userMileageEntries.Take(request.PageSize.Value);
            }

            var results = await userMileageEntries.ToListAsync(cancellationToken);

            return new LeaderBoardQueryResponse
            {
                Entries = results.Select((x, i) => new LeaderBoardEntryModel
                {
                    Place = i + 1,
                    GravatarUrl = gravatarResolver.GetGravatarUrl(x.User?.Email),
                    SkaterName = x.User?.GetDisplaySkaterName(),
                    TotalMiles = x.Distance.TotalMiles
                }).ToList()
            };
        }
    }
}
