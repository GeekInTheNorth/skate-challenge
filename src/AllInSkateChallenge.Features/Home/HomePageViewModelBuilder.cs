using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : PageViewModelBuilder<HomePageViewModel>, IHomePageViewModelBuilder
    {
        private readonly ISummaryStatisticsRepository summaryStatisticsRepository;

        private readonly IMediator mediator;

        public HomePageViewModelBuilder(ISummaryStatisticsRepository summaryStatisticsRepository, IMediator mediator, ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
            this.summaryStatisticsRepository = summaryStatisticsRepository;
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<HomePageViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Home";
            model.DisplayPageTitle = "Welcome to the ALL IN Skate Challenge";
            
            var summary = summaryStatisticsRepository.Get();
            model.IntroductoryText = $"The ALL IN Skate Challenge is to skate the equivelent of the Leeds Liverpool Canal over the period of one month. {summary.NumberOfSkaters} skaters have now taken up the challenge and skated a collective {summary.TotalMiles:F1} miles.";

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
