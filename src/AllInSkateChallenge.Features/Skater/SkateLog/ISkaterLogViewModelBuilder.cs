using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public interface ISkaterLogViewModelBuilder : IPageViewModelBuilder<SkaterLogViewModel>
    {
        IPageViewModelBuilder<SkaterLogViewModel> WithNewEntry(INewSkaterLogEntry newEntry);
    }
}
