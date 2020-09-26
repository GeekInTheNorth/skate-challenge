using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public interface ISkaterProgressViewModelBuilder
    {
        ISkaterProgressViewModelBuilder WithUser(ApplicationUser applicationUser);

        SkaterProgressViewModel Build();
    }
}
