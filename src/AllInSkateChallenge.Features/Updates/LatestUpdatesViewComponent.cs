using AllInSkateChallenge.Features.Framework.Routing;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewComponent : ViewComponent
    {
        private readonly RouteSettings routeSettings;

        public LatestUpdatesViewComponent(IOptions<RouteSettings> routeSettings)
        {
            this.routeSettings = routeSettings.Value;
        }

        public IViewComponentResult Invoke(int take, int skip, bool showLoadMore, bool showJourneyName)
        {
            var model = new LatestUpdatesViewComponentModel
            {
                Take = take,
                Skip = skip,
                ShowLoadMore = showLoadMore,
                ShowJourneyName = showJourneyName,
                LatestUpdatesUrl = routeSettings.LatestUpdatesUrl
            };

            return View("~/Views/Shared/Components/LatestUpdates/Default.cshtml", model);
        }
    }
}
