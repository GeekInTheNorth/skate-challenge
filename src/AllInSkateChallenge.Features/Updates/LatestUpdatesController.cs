using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Updates
{
    [Authorize]
    public class LatestUpdatesController : Controller
    {
        private readonly ILatestUpdatesViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public LatestUpdatesController(ILatestUpdatesViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await userManager.GetUserAsync(User);

            var model = await viewModelBuilder.WithPaging(page, 20).WithUser(user).Build();

            return View(model);
        }
    }
}