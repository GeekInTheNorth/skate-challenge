using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class StatisticLeadersViewModelBuilder : PageViewModelBuilder<StatisticLeadersViewModel>, IStatisticLeadersViewModelBuilder
    {
        private readonly IMediator mediator;

        private StatisticType statisticType;

        public StatisticLeadersViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
            this.statisticType = StatisticType.BestAverageSpeed;
        }

        public override async Task<PageViewModel<StatisticLeadersViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Event Statistics";
            model.DisplayPageTitle = "Event Statistics";

            var query = new EventStatisticsQuery();
            var result = await mediator.Send(query);

            switch (statisticType)
            {
                case StatisticType.BestAverageSpeed:
                    model.Content.Skaters = await mediator.Send(new GetBestAverageSpeedLeadersQuery());
                    model.PageTitle = "Skaters with the Best Average Speed";
                    model.DisplayPageTitle = "Skaters with the Best Average Speed";
                    model.IntroductoryText = "Skaters must have submitted a skating work out with a duration of at least 30 minutes in order to qualify for this leaderboard.";
                    model.Content.StatisticTitle = "Average Speed";
                    break;
                case StatisticType.BestTopSpeed:
                    model.Content.Skaters = await mediator.Send(new GetBestTopSpeedLeadersQuery());
                    model.PageTitle = "Skaters with the Best Top Speed";
                    model.DisplayPageTitle = "Skaters with the Best Top Speed";
                    model.Content.StatisticTitle = "Top Speed";
                    break;
                case StatisticType.LongestDistance:
                    model.Content.Skaters = await mediator.Send(new GetLongestJourneyLeadersQuery());
                    model.PageTitle = "Skaters with the Longest Journeys";
                    model.DisplayPageTitle = "Skaters with the Longest Journeys";
                    model.IntroductoryText = "Skaters are shown here with the longest distance achieved in a single skate journey.";
                    model.Content.StatisticTitle = "Distance";
                    break;
                default:
                    throw new NotImplementedException($"{statisticType} is not currently supported");
            }

            return model;
        }

        public IPageViewModelBuilder<StatisticLeadersViewModel> WithStatisticType(StatisticType statisticType)
        {
            this.statisticType = statisticType;
            return this;
        }
    }

    [Authorize]
    public class StatisticLeadersController : Controller
    {
        private readonly IStatisticLeadersViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public StatisticLeadersController(IStatisticLeadersViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        [Route("EventStatistics/Leaders/{statisticType}")]
        public async Task<IActionResult> Index(StatisticType statisticType)
        {
            var userDetails = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithStatisticType(statisticType).WithUser(userDetails).Build();

            return View(model);
        }
    }
}
