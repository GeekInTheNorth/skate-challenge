using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

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
}
