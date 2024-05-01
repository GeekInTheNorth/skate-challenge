using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Extensions;

using Kontent.Ai.Delivery.Abstractions;
using Kontent.Ai.Urls.Delivery.QueryParameters;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class HomePageRepository(IDeliveryClient deliveryClient) : IHomePageRepository
{
    private HomePageModel _homePageModel = null;

    public async Task<HomePageModel> GetAsync()
    {
        if (_homePageModel is not null)
        {
            return _homePageModel;
        }

        var response = await deliveryClient.GetItemsAsync<HomePageData>(
            new OrderParameter("system.last_modified", SortOrder.Descending),
            new LimitParameter(1));

        _homePageModel = response.Items
                                 .Select(x => new HomePageModel
                                 {
                                     Title = x.Title,
                                     Introduction = x.Introduction,
                                     RegistrationTitle = x.RegistrationTitle,
                                     RegistrationGuidance = x.RegistrationGuidance,
                                     EventMap = x.EventMap.GetSingleUrl()
                                 }).FirstOrDefault();

        return _homePageModel;
    }
}