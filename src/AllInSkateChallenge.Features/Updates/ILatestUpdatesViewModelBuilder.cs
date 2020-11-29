using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Updates
{
    public interface ILatestUpdatesViewModelBuilder : IPageViewModelBuilder<LatestUpdatesViewModel>
    {
        ILatestUpdatesViewModelBuilder WithPaging(int page, int pageSize);
    }
}