using System.Collections.Generic;

using AllInSkateChallenge.Features.Gravatar;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQuery : ILeaderBoardQuery
    {
        private readonly IGravatarResolver gravatarResolver;

        public LeaderBoardQuery(IGravatarResolver gravatarResolver)
        {
            this.gravatarResolver = gravatarResolver;
        }

        public LeaderBoardModel Get()
        {
            return new LeaderBoardModel
            {
                Entries = new List<LeaderBoardEntryModel>
                {
                    new LeaderBoardEntryModel { Place = 1, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 1000 },
                    new LeaderBoardEntryModel { Place = 2, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 900 },
                    new LeaderBoardEntryModel { Place = 3, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 800 },
                    new LeaderBoardEntryModel { Place = 4, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 700 },
                    new LeaderBoardEntryModel { Place = 5, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 600 },
                    new LeaderBoardEntryModel { Place = 6, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 500 },
                    new LeaderBoardEntryModel { Place = 7, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 400 },
                    new LeaderBoardEntryModel { Place = 8, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 300 },
                    new LeaderBoardEntryModel { Place = 9, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 200 },
                    new LeaderBoardEntryModel { Place = 999, GravatarUrl = gravatarResolver.GetGravatarUrl("joebloggs@example.com"), Name = "Joe Bloggs", TotalMiles = 100 }
                }
            };
        }
    }
}
