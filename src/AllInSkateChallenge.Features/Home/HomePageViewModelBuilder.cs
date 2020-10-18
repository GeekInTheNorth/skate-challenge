using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

using MediatR;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : IHomePageViewModelBuilder
    {
        private readonly ILeaderBoardQuery leaderBoardQuery;

        private readonly ISummaryStatisticsRepository summaryStatisticsRepository;

        private readonly IMediator mediator;

        private ApplicationUser skater;

        public HomePageViewModelBuilder(ILeaderBoardQuery leaderBoardQuery, ISummaryStatisticsRepository summaryStatisticsRepository, IMediator mediator)
        {
            this.leaderBoardQuery = leaderBoardQuery;
            this.summaryStatisticsRepository = summaryStatisticsRepository;
            this.mediator = mediator;
        }

        public IHomePageViewModelBuilder WithUser(ApplicationUser skater)
        {
            this.skater = skater;

            return this;
        }

        public async Task<HomePageViewModel> Build()
        {
            var model = new HomePageViewModel();

            model.ShowSignUpPromotion = skater == null;
            model.SummaryStatistics = summaryStatisticsRepository.Get();

            var latestUpdates = await mediator.Send(new LatestUpdatesQuery { Limit = 10 });

            if (skater != null)
            {
                model.LeaderBoard = leaderBoardQuery.Get();
                model.LatestUpdates = latestUpdates.Entries;
            }

            return model;
        }
    }
}
