using System.Linq;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Gravatar;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQuery : ILatestUpdatesQuery
    {
        private readonly ApplicationDbContext context;

        private readonly IGravatarResolver gravatarResolver;

        public LatestUpdatesQuery(ApplicationDbContext context, IGravatarResolver gravatarResolver)
        {
            this.context = context;
            this.gravatarResolver = gravatarResolver;
        }

        public MileageUpdateModel Get(int maximum = 10)
        {
            var userMilageEntries = from mileageEntry in context.MileageEntries
                                    join user in context.Users on mileageEntry.UserId.ToString() equals user.Id into userJoin
                                    from nullableUser in userJoin.DefaultIfEmpty()
                                    orderby mileageEntry.Logged descending
                                    select new
                                    {
                                        MileageEntry = mileageEntry,
                                        User = nullableUser
                                    };

            var results = userMilageEntries.Take(maximum).ToList();

            return new MileageUpdateModel
            {
                Entries = results.Select(x => new MileageUpdateEntryModel
                {
                    Logged = x.MileageEntry.Logged,
                    Miles = x.MileageEntry.Miles,
                    GravatarUrl = gravatarResolver.GetGravatarUrl(x.User?.Email),
                    Skater = x.User?.UserName ?? "Private Skater",
                }).ToList()
            };
        }
    }
}
