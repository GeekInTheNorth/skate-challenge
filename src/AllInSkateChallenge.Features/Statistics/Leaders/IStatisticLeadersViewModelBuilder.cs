
using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public interface IStatisticLeadersViewModelBuilder : IPageViewModelBuilder<StatisticLeadersViewModel>
    {
        IPageViewModelBuilder<StatisticLeadersViewModel> WithStatisticType(StatisticType statisticType);
    }
}
