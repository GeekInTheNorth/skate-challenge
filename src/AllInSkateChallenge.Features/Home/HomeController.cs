using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IHomePageViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IHomePageViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var skater = await userManager.GetUserAsync(User);

            var model = await viewModelBuilder.WithUser(skater).Build();

            return View(model);
        }
    }
}
