namespace AllInSkateChallenge.Features.Statistics
{
    using System;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Framework.Models;

    using MediatR;

    public class EventStatisticsViewModelBuilder : PageViewModelBuilder<EventStatisticsViewModel>, IEventStatisticsViewModelBuilder
    {
        private readonly IMediator mediator;

        private PeriodRange periodRange;

        public EventStatisticsViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
            periodRange = PeriodRange.AllTime;
        }

        public override async Task<PageViewModel<EventStatisticsViewModel>> Build()
        {
            var model = await base.Build();
            var query = new EventStatisticsQuery();

            switch (periodRange)
            {
                case PeriodRange.PreviousMonth:
                    var previousMonth = DateTime.Today.AddMonths(-1);
                    query.DateFrom = new DateTime(previousMonth.Year, previousMonth.Month, 1);
                    query.DateTo = query.DateFrom.Value.AddMonths(1).AddMilliseconds(-1);
                    model.PageTitle = "Event Statistics - Last Month";
                    model.DisplayPageTitle = "Event Statistics - Last Month";
                    model.IntroductoryText = $"The following statistics are based entirely on journeys made by our skaters in the previous calendar month ({previousMonth:Y})";
                    break;
                case PeriodRange.CurrentMonth:
                    query.DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    query.DateTo = DateTime.UtcNow;
                    model.PageTitle = "Event Statistics - Current Month";
                    model.DisplayPageTitle = "Event Statistics - Current Month";
                    model.IntroductoryText = $"The following statistics are based entirely on journeys made by our skaters in the current calendar month ({DateTime.Today:Y})";
                    break;
                default:
                    model.PageTitle = "Event Statistics";
                    model.DisplayPageTitle = "Event Statistics";
                    break;
            }

            var result = await mediator.Send(query);

            model.Content.PeriodRange = periodRange;
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

        public IPageViewModelBuilder<EventStatisticsViewModel> WithPeriodRange(PeriodRange periodRange)
        {
            this.periodRange = periodRange;

            return this;
        }
    }
}