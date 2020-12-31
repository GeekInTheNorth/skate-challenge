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
            model.Content.SkateDistances = result.SkateDistances;
            model.Content.SkateSessions = result.SkateSessions;
            model.Content.TotalMiles = result.TotalMiles;
            model.Content.TotalSkateSessions = result.TotalSkateSessions;
            model.Content.MilesByManual = result.MilesByManual;
            model.Content.MilesByStrava = result.MilesByStrava;
            model.Content.JourneysByManual = result.JourneysByManual;
            model.Content.JourneysByStrava = result.JourneysByStrava;

            return model;
        }
    }
}