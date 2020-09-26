using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.Progress;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Controllers
{
    [Authorize]
    public class SkaterController : Controller
    {
        private readonly ISkaterProgressViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public SkaterController(ISkaterProgressViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Progress()
        {
            var user = await userManager.GetUserAsync(User);

            var model = viewModelBuilder.WithUser(user).Build();

            return View(model);
        }
    }
}
