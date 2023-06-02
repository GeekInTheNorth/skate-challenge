namespace AllInSkateChallenge.Features.Home;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IHomePageViewModelBuilder viewModelBuilder;

    private readonly UserManager<ApplicationUser> userManager;

    public HomeController(IHomePageViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
    {
        this.viewModelBuilder = viewModelBuilder;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var stravaDetails = await userManager.GetStravaDetails(User);

        var model = await viewModelBuilder.WithUser(stravaDetails).Build();

        return View(model);
    }
}