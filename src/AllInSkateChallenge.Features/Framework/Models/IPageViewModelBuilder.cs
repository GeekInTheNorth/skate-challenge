using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Framework.Models
{
    public interface IPageViewModelBuilder<T>
        where T : class
    {
        IPageViewModelBuilder<T> WithUser(ApplicationUser user);

        Task<PageViewModel<T>> Build();
    }
}
