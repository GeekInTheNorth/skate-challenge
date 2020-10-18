using System.Threading.Tasks;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Home
{
    public interface IHomePageViewModelBuilder
    {
        IHomePageViewModelBuilder WithUser(ApplicationUser skater);

        Task<HomePageViewModel> Build();
    }
}
