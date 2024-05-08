using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.SkateTeam.Progress;

[Authorize]
public sealed class TeamProgressController(ITeamProgressViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    [Route("team/progress")]
    public async Task<IActionResult> Progress()
    {
        var user = await userManager.GetStravaDetails(User);

        var model = await viewModelBuilder.WithUser(user).Build();

        return View("~/Views/SkateTeam/Progress.cshtml", model);
    }
}