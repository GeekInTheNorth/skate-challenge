using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public interface ISkaterProgressViewModelBuilder
    {
        ISkaterProgressViewModelBuilder WithUser(ApplicationUser applicationUser);

        Task<SkaterProgressViewModel> Build();
    }
}
