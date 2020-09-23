using System.Threading.Tasks;
using AllInSkateChallenge.Features.MileageLogging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Controllers
{
    [Authorize]
    public class MileageLoggingController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly IMileageLoggingRepository repository;

        public MileageLoggingController(UserManager<IdentityUser> userManager, IMileageLoggingRepository repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var model = new MileageLoggingEntryModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm]MileageLoggingEntryModel mileageEntry)
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
