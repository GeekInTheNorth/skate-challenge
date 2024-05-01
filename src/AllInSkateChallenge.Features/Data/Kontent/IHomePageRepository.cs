using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.Data.Kontent;

public interface IHomePageRepository
{
    Task<HomePageModel> GetAsync();
}
