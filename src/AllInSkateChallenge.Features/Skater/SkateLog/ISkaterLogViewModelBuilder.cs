using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public interface ISkaterLogViewModelBuilder : IPageViewModelBuilder<SkaterLogViewModel>
    {
        ISkaterLogViewModelBuilder WithNewEntry(INewSkaterLogEntry newEntry);

        ISkaterLogViewModelBuilder WithSaveResponse(SaveActivityCommandResult saveResponse);
    }
}
