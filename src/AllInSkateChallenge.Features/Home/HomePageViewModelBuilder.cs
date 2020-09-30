using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : IHomePageViewModelBuilder
    {
        private readonly ILeaderBoardQuery leaderBoardQuery;

        private readonly ILatestUpdatesQuery latestUpdatesQuery;

        private ApplicationUser skater;

        public HomePageViewModelBuilder(ILeaderBoardQuery leaderBoardQuery, ILatestUpdatesQuery latestUpdatesQuery)
        {
            this.leaderBoardQuery = leaderBoardQuery;
            this.latestUpdatesQuery = latestUpdatesQuery;
        }

        public IHomePageViewModelBuilder WithUser(ApplicationUser skater)
        {
            this.skater = skater;

            return this;
        }

        public HomePageViewModel Build()
        {
            var model = new HomePageViewModel();

            model.ShowSignUpPromotion = skater == null;

            if (skater != null)
            {
                model.LeaderBoard = leaderBoardQuery.Get();
                model.LatestUpdates = latestUpdatesQuery.Get();
            }

            return model;
        }
    }
}
