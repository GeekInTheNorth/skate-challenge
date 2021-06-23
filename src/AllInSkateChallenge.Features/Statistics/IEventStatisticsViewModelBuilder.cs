namespace AllInSkateChallenge.Features.Statistics
{
    using AllInSkateChallenge.Features.Framework.Models;

    public interface IEventStatisticsViewModelBuilder : IPageViewModelBuilder<EventStatisticsViewModel>
    {
        IPageViewModelBuilder<EventStatisticsViewModel> WithPeriodRange(PeriodRange periodRange);
    }
}