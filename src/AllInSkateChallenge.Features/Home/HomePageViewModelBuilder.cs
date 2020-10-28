using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

using MediatR;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : PageViewModelBuilder<HomePageViewModel>, IHomePageViewModelBuilder
    {
        private readonly ISummaryStatisticsRepository summaryStatisticsRepository;

        private readonly IMediator mediator;

        public HomePageViewModelBuilder(ISummaryStatisticsRepository summaryStatisticsRepository, IMediator mediator) : base(mediator)
        {
            this.summaryStatisticsRepository = summaryStatisticsRepository;
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<HomePageViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Home";
            model.DisplayPageTitle = "Welcome to the ALL IN Leeds-Liverpool Skate Challenge";
            model.Content.Summary = summaryStatisticsRepository.Get();
            
            if (model.IsLoggedIn)
            {
                var latestUpdates = await mediator.Send(new LatestUpdatesQuery { Limit = 10 });
                var leaderBoard = await mediator.Send(new LeaderBoardQuery { Limit = 10 });

                model.Content.LeaderBoard = leaderBoard.Entries;
                model.Content.LatestUpdates = latestUpdates.Entries;
            }

            return model;
        }
    }
}
