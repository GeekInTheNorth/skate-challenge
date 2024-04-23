namespace AllInSkateChallenge.Features.Home;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.SkateTeam;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public sealed class HomeController(
    IHomePageViewModelBuilder viewModelBuilder,
    ISkateTeamRepository skateTeamRepository, 
    UserManager<ApplicationUser> userManager) : Controller
{
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> Index()
    {
        var stravaDetails = await userManager.GetStravaDetails(User);

        var model = await viewModelBuilder.WithUser(stravaDetails).Build();

        if (User.Identity.IsAuthenticated)
        { 
            if (!User.IsInRole("Administrator"))
            {
                var user = await userManager.GetUserAsync(User);
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        var teams = await skateTeamRepository.GetAsync();

        return View(model);
    }
}