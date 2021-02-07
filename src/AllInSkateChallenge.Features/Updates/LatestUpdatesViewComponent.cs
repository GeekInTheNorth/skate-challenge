using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int take, int skip, bool showLoadMore)
        {
            var model = new LatestUpdatesViewComponentModel
            {
                Take = take,
                Skip = skip,
                ShowLoadMore = showLoadMore,
                LatestUpdatesUrl = "https://allinskatechallengefunctions.azurewebsites.net/api/SkaterLog/List/"
            };

            return View("~/Views/Shared/Components/LatestUpdates/Default.cshtml", model);
        }
    }
}
