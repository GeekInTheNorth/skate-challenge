using AllInSkateChallenge.Features.MileageLogging;

using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Controllers
{
    public class MileageLoggingController : Controller
    {
        public IActionResult Index()
        {
            var model = new MileageLoggingEntryModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm]MileageLoggingEntryModel mileageEntry)
        {
            if (!TryValidateModel(mileageEntry, nameof(MileageLoggingEntryModel)))
            {
                return View(mileageEntry);
            }

            return View("~/Views/MileageLogging/Success.cshtml", mileageEntry);
        }
    }
}
