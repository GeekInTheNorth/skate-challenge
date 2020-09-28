using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Skater.MileageLogging
{
    [Authorize]
    public class MileageLoggingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ISkaterMileageEntriesRepository repository;

        public MileageLoggingController(UserManager<ApplicationUser> userManager, ISkaterMileageEntriesRepository repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        [Route("skater/skate-log/add")]
        public IActionResult Index()
        {
            var model = new MileageLoggingEntryModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skater/skate-log/add")]
        public async Task<IActionResult> Index([FromForm] MileageLoggingEntryModel mileageEntry)
        {
            if (!TryValidateModel(mileageEntry, nameof(MileageLoggingEntryModel)))
            {
                return View(mileageEntry);
            }

            var user = await userManager.GetUserAsync(User);
            await repository.SaveAsync(user, mileageEntry);

            return View("~/Views/MileageLogging/Success.cshtml", mileageEntry);
        }
    }
}
