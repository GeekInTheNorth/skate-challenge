namespace AllInSkateChallenge.Features.Statistics
{
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Framework.Models;

    using MediatR;

    public class EventStatisticsViewModelBuilder : PageViewModelBuilder<EventStatisticsViewModel>, IEventStatisticsViewModelBuilder
    {
        private readonly IMediator mediator;

        public EventStatisticsViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<EventStatisticsViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Event Statistics";
            model.DisplayPageTitle = "Event Statistics";

            var query = new EventStatisticsQuery();
            var result = await mediator.Send(query);

            model.Content.LongestTotalDistance = result.LongestTotalDistance;
            model.Content.LongestSingleDistance = result.LongestSingleDistance;
            model.Content.ShortestSingleDistance = result.ShortestSingleDistance;
            model.Content.MostJourneys = result.MostJourneys;
            model.Content.SkateDistances = result.SkateDistances;
            model.Content.SkateSessions = result.SkateSessions;
            model.Content.BestTopSpeed = result.BestTopSpeed;
            model.Content.BestAverageSpeed = result.BestAverageSpeed;
            model.Content.GreatestClimb = result.GreatestClimb;
            model.Content.SkybornSkater = result.SkybornSkater;
            model.Content.ActivitiesByDay = result.ActivitiesByDay;
            model.Content.MilesByDay = result.MilesByDay;
            model.Content.TotalMiles = result.TotalMiles;
            model.Content.TotalSkateSessions = result.TotalSkateSessions;
            model.Content.MilesByManual = result.MilesByManual;
            model.Content.MilesByStrava = result.MilesByStrava;
            model.Content.JourneysByManual = result.JourneysByManual;
            model.Content.JourneysByStrava = result.JourneysByStrava;
            model.Content.CheckPoints = result.CheckPoints;

            return model;
        }
    }
}