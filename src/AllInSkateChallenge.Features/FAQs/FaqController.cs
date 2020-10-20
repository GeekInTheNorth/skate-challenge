using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.FAQs
{
    public class FaqController : Controller
    {
        private readonly IFaqViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public FaqController(IFaqViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
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
