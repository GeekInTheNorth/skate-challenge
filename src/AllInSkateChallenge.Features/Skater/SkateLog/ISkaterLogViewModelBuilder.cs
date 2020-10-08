using System.Threading.Tasks;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public interface ISkaterLogViewModelBuilder
    {
        ISkaterLogViewModelBuilder WithNewEntry(INewSkaterLogEntry newEntry);

        ISkaterLogViewModelBuilder WithUser(ApplicationUser skater);

        Task<SkaterLogViewModel> Build();
    }
}
