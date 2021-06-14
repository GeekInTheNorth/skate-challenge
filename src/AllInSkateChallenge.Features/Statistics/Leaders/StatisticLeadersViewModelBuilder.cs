using System;
using System.Linq;
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

            var query = new StatisticLeaderQuery { StatisticType = statisticType };
            model.Content.Skaters = (await mediator.Send(query)).ToList();

            switch (statisticType)
            {
                case StatisticType.BestAverageSpeed:
                    model.PageTitle = "Skaters with the Best Average Speed";
                    model.DisplayPageTitle = "Skaters with the Best Average Speed";
                    model.IntroductoryText = "Skaters must have submitted a skating work out with a duration of at least 30 minutes in order to qualify for this leaderboard.";
                    model.Content.StatisticTitle = "Average Speed";
                    break;
                case StatisticType.BestTopSpeed:
                    model.PageTitle = "Skaters with the Best Top Speed";
                    model.DisplayPageTitle = "Skaters with the Best Top Speed";
                    model.Content.StatisticTitle = "Top Speed";
                    break;
                case StatisticType.LongestDistance:
                    model.PageTitle = "Skaters with the Longest Journeys";
                    model.DisplayPageTitle = "Skaters with the Longest Journeys";
                    model.IntroductoryText = "Skaters are shown here with the longest distance achieved in a single skate journey.";
                    model.Content.StatisticTitle = "Distance";
                    break;
                case StatisticType.ShortestDistance:
                    model.PageTitle = "Skaters with the Shortest Journeys";
                    model.DisplayPageTitle = "Skaters with the Shortest Journeys";
                    model.IntroductoryText = "Skaters are shown here with the shortest distance achieved in a single skate journey.";
                    model.Content.StatisticTitle = "Distance";
                    break;
                case StatisticType.GreatestElevationGain:
                    model.PageTitle = "Skaters with the Greatest Elevation Gain";
                    model.DisplayPageTitle = "Skaters with the Greatest Elevation Gain";
                    model.IntroductoryText = "Skaters are shown here with the greatest elevation gain achieved in a single skate journey.";
                    model.Content.StatisticTitle = "Elevation";
                    break;
                case StatisticType.HighestElevation:
                    model.PageTitle = "Skaters with the Highest Elevation";
                    model.DisplayPageTitle = "Skaters with the Highest Elevation";
                    model.IntroductoryText = "Skaters are shown here with the highest elevation visited across all journeys.";
                    model.Content.StatisticTitle = "Elevation";
                    break;
                case StatisticType.LongestTotalJourney:
                    model.PageTitle = "Skaters with Longest Total Journey";
                    model.DisplayPageTitle = "Skaters with Longest Total Journey";
                    model.IntroductoryText = "Skaters are shown here with the greatest cumulative distance across all their journeys.";
                    model.Content.StatisticTitle = "Distance";
                    break;
                case StatisticType.MostJourneys:
                    model.PageTitle = "Skaters with Most Journeys";
                    model.DisplayPageTitle = "Skaters with Most Journeys";
                    model.IntroductoryText = "Skaters are shown here with the greatest number of journeys.";
                    model.Content.StatisticTitle = "Journeys";
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
