using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Privacy
{
    public class PrivacyController : Controller
    {
        private readonly IPrivacyViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public PrivacyController(IPrivacyViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            var model = await viewModelBuilder.WithUser(user).Build();

            return View(model);
        }
    }
}
