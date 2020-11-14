using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Error
{
    public class ErrorController : Controller
    {
        private readonly IErrorViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public ErrorController(IErrorViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        [Route("error")]
        [Route("error/{statusCode}")]
        public async Task<IActionResult> Index(int statusCode = 0)
        {
            var skater = await userManager.GetUserAsync(User);

            var model = await viewModelBuilder.WithStatusCode(statusCode).WithUser(skater).Build();

            return View(model);
        }
    }
}
