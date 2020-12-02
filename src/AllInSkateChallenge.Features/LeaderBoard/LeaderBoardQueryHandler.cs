using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Gravatar;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQueryHandler : IRequestHandler<LeaderBoardQuery, LeaderBoardQueryResponse>
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        private readonly ICheckPointRepository checkPointRepository;

        public LeaderBoardQueryHandler(
            ApplicationDbContext context,
            IGravatarResolver gravatarResolver, 
            ICheckPointRepository checkPointRepository)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
            this.checkPointRepository = checkPointRepository;
        }

        public async Task<LeaderBoardQueryResponse> Handle(LeaderBoardQuery request, CancellationToken cancellationToken)
        {
            var targetDistance = checkPointRepository.Get().FirstOrDefault(x => x.SkateTarget.Equals(request.Target))?.Distance ?? 127.5M;
            var distanceTotals = from entries in context.SkateLogEntries
                                 group entries by entries.ApplicationUserId into userEntries
                                 select new
                                 {
                                     UserId = userEntries.Key,
                                     TotalMiles = userEntries.Sum(x => x.DistanceInMiles)
                                 };

            var userMilageEntries = from distanceTotal in distanceTotals
                                    join user in context.Users on distanceTotal.UserId equals user.Id
                                    where user.HasPaid == true && (user.Target <= request.Target || distanceTotal.TotalMiles <= targetDistance)
                                    orderby distanceTotal.TotalMiles descending
                                    select new
                                    {
                                        Distance = distanceTotal,
                                        User = user
                                    };

            if (request.PageSize.HasValue)
            {
                userMilageEntries = userMilageEntries.Take(request.PageSize.Value);
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
