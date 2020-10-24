using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : PageViewModelBuilder<HomePageViewModel>, IHomePageViewModelBuilder
    {
        private readonly ISummaryStatisticsRepository summaryStatisticsRepository;

        private readonly IMediator mediator;

        public HomePageViewModelBuilder(ISummaryStatisticsRepository summaryStatisticsRepository, IMediator mediator, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
            this.summaryStatisticsRepository = summaryStatisticsRepository;
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<HomePageViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Home";
            model.DisplayPageTitle = "Welcome to the ALL IN Leeds-Liverpool Skate Challenge";
            
            var summary = summaryStatisticsRepository.Get();
            model.IntroductoryText = $"ALL IN challenges you to join our virtual skate along the Leeds-Liverpool canal (127.5 miles)! The Leeds Liverpool canal is the longest canal in Britain built as a single waterway and snakes through the northern heartlands of the Industrial Revolution, including right through our very own Bradford! Skate whenever, wherever, and take however long you need. Track your miles here and share your succeses in our Facebook Group! Disclaimer: the actual Leeds-Liverpool canal isn't skateable... you'll need a boat! {summary.NumberOfSkaters} skaters have now taken up the challenge and skated a collective {summary.TotalMiles:F1} miles.";

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
