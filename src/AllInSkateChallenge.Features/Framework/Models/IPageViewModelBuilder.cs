namespace AllInSkateChallenge.Features.Framework.Models;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

public interface IPageViewModelBuilder<T>
    where T : class
{
    IPageViewModelBuilder<T> WithUser(ApplicationUser user);

    IPageViewModelBuilder<T> WithUser(StavaDetails user);

    Task<PageViewModel<T>> Build();
}