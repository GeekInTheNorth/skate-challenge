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
            var userMilageEntries = from skateLogEntry in context.SkateLogEntries
                                    join user in context.Users on skateLogEntry.ApplicationUserId equals user.Id
                                    orderby skateLogEntry.Logged descending
                                    select new
                                    {
                                        Entry = skateLogEntry,
                                        User = user
                                    };

            var results = userMilageEntries.Take(maximum).ToList();

            return new MileageUpdateModel
            {
                Entries = results.Select(x => new MileageUpdateEntryModel
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
