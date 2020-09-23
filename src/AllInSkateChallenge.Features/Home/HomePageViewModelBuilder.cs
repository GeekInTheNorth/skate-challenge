using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : IHomePageViewModelBuilder
    {
        private readonly ILeaderBoardQuery leaderBoardQuery;

        private readonly ILatestUpdatesQuery latestUpdatesQuery;

        public HomePageViewModelBuilder(ILeaderBoardQuery leaderBoardQuery, ILatestUpdatesQuery latestUpdatesQuery)
        {
            this.leaderBoardQuery = leaderBoardQuery;
            this.latestUpdatesQuery = latestUpdatesQuery;
        }

        public HomePageViewModel Build()
        {
            return new HomePageViewModel
            {
                LeaderBoard = leaderBoardQuery.Get(),
                LatestUpdates = latestUpdatesQuery.Get()
            };
        }
    }
}
