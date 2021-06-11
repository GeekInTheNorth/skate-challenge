using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    [Authorize]
    public class StatisticLeadersController : Controller
    {
        private readonly IStatisticLeadersViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public StatisticLeadersController(IStatisticLeadersViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        [Route("EventStatistics/Leaders/{statisticType}")]
        public async Task<IActionResult> Index(StatisticType statisticType)
        {
            var userDetails = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithStatisticType(statisticType).WithUser(userDetails).Build();

            return View(model);
        }
    }
}
