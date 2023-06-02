namespace AllInSkateChallenge.Features.Skater.Progress;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class SkaterProgressController : Controller
{
    private readonly ISkaterProgressViewModelBuilder viewModelBuilder;

    private readonly UserManager<ApplicationUser> userManager;

    public SkaterProgressController(ISkaterProgressViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
    {
        this.viewModelBuilder = viewModelBuilder;
        this.userManager = userManager;
    }

    [Route("skater/progress")]
    public async Task<IActionResult> Progress()
    {
        var user = await userManager.GetStravaDetails(User);

        var model = await viewModelBuilder.WithUser(user).Build();

        return View("~/Views/Skater/Progress.cshtml", model);
    }
}