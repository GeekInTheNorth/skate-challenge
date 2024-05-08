using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.SkateTeam;

[Authorize]
public sealed class SkateTeamSelectionController (
    ISkateTeamRepository skateTeamRepository,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    [Route("team/options")]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is not { Team: 0 })
        {
            return Redirect("/");
        }
        
        var teams = await skateTeamRepository.GetAsync();

        return View("~/Views/SkateTeam/Selection.cshtml", teams);
    }

    [HttpPost]
    [Route("team/select")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Select(int skateTeam)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is not { Team: 0 })
        {
            return Redirect("/");
        }

        var teams = await skateTeamRepository.GetAsync();
        var chosenTeam = teams.FirstOrDefault(x => x.Id == skateTeam);

        if (chosenTeam is not null)
        {
            user.Team = skateTeam;
            await userManager.UpdateAsync(user);
        }

        return Redirect("/");
    }
}
