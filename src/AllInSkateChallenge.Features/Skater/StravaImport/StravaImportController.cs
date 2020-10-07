using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    [Authorize]
    public class StravaImportController : Controller
    {
        private readonly IStravaImportViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public StravaImportController(IStravaImportViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        [Route("skater/skate-log/strava-import")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            // Only strava users should be able to get here
            if (!user.IsStravaAccount)
            {
                return Redirect("/");
            }

            var model = await viewModelBuilder.WithUser(user).BuildAsync();

            return View("~/Views/Skater/StravaImport.cshtml", model);
        }
    }
}
