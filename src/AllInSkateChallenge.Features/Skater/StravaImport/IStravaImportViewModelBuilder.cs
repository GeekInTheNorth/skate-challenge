using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public interface IStravaImportViewModelBuilder
    {
        IStravaImportViewModelBuilder WithUser(ApplicationUser skater);

        Task<StravaImportViewModel> BuildAsync();
    }
}
