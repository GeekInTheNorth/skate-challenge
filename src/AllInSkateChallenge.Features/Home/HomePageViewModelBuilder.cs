using AllInSkateChallenge.Features.LeaderBoard;

namespace AllInSkateChallenge.Features.Home
{

    public class HomePageViewModelBuilder : IHomePageViewModelBuilder
    {
        private readonly ILeaderBoardQuery leaderBoardQuery;

        public HomePageViewModelBuilder(ILeaderBoardQuery leaderBoardQuery)
        {
            this.leaderBoardQuery = leaderBoardQuery;
        }

        public HomePageViewModel Build()
        {
            return new HomePageViewModel
            {
                LeaderBoard = leaderBoardQuery.Get()
            };
        }
    }
}
