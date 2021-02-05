using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    [Authorize]
    public class LeaderBoardController : Controller
    {
        private readonly ILeaderBoardViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public LeaderBoardController(ILeaderBoardViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
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
