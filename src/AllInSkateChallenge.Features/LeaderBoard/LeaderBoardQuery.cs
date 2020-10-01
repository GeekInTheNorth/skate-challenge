using System.Linq;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Gravatar;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQuery : ILeaderBoardQuery
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public LeaderBoardQuery(
            ApplicationDbContext context,
            IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public LeaderBoardModel Get(int size = 10)
        {
            var distanceTotals = from entries in context.MileageEntries
                                 group entries by entries.UserId into userEntries
                                 select new
                                 {
                                     UserId = userEntries.Key,
                                     TotalMiles = userEntries.Sum(x => x.Miles),
                                     TotalKilometres = userEntries.Sum(x => x.Kilometres)
                                 };

            var userMilageEntries = from distanceTotal in distanceTotals
                                    join user in context.Users on distanceTotal.UserId.ToString() equals user.Id into userJoin
                                    from nullableUser in userJoin.DefaultIfEmpty()
                                    orderby distanceTotal.TotalMiles descending
                                    select new
                                    {
                                        Distance = distanceTotal,
                                        User = nullableUser
                                    };

            var results = userMilageEntries.Take(size).ToList();

            return new LeaderBoardModel
            {
                Entries = results.Select((x, i) => new LeaderBoardEntryModel
                {
                    Place = i + 1,
                    GravatarUrl = gravatarResolver.GetGravatarUrl(x.User?.Email),
                    Name = x.User?.SkaterName ?? "Private Skater",
                    TotalMiles = x.Distance.TotalMiles
                }).ToList()
            };
        }
    }
}
