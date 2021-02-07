using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int take, int skip)
        {
            var model = new LatestUpdatesViewComponentModel
            {
                Take = take,
                Skip = skip,
                LatestUpdatesUrl = "https://allinskatechallengefunctions.azurewebsites.net/api/SkaterLog/List/"
            };

            return View("~/Views/Shared/Components/LatestUpdates/Default.cshtml", model);
        }
    }
}
