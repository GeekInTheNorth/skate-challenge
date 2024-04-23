namespace AllInSkateChallenge.Features.Skater.Progress;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.SkateTeam;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public sealed class SkaterProgressController(ISkaterProgressViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager) : Controller
{
    [Route("skater/progress")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> Progress()
    {
        var user = await userManager.GetStravaDetails(User);

        var model = await viewModelBuilder.WithUser(user).Build();

        return View("~/Views/Skater/Progress.cshtml", model);
    }
}