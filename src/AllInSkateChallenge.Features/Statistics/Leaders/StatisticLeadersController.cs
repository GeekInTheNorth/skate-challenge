using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.SkateTeam;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Statistics.Leaders;

[Authorize]
public sealed class StatisticLeadersController(
    IStatisticLeadersViewModelBuilder viewModelBuilder, 
    UserManager<ApplicationUser> userManager) : Controller
{
    [Route("EventStatistics/Leaders/{statisticType}")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> Index(StatisticType statisticType)
    {
        var userDetails = await userManager.GetUserAsync(User);
        var model = await viewModelBuilder.WithStatisticType(statisticType).WithUser(userDetails).Build();

        return View(model);
    }
}
