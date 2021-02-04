using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Routing;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardViewComponent : ViewComponent
    {
        private readonly ICheckPointRepository checkPointRepository;

        private readonly RouteSettings routeSettings;

        public LeaderBoardViewComponent(ICheckPointRepository checkPointRepository, IOptions<RouteSettings> routeSettings)
        {
            this.checkPointRepository = checkPointRepository;
            this.routeSettings = routeSettings.Value;
        }

        public IViewComponentResult Invoke(ApplicationUser user, bool showFilter, int? limit)
        {
            var model = new LeaderBoardViewComponentModel
            {
                ShowFilter = showFilter,
                FilterItems = checkPointRepository.GetSelectList(),
                FilterValue = user.Target,
                Limit = limit,
                LeaderBoardUrl = routeSettings.LeaderBoardUrl
            };

            return View("~/Views/Shared/Components/LeaderBoard/Default.cshtml", model);
        }
    }
}
