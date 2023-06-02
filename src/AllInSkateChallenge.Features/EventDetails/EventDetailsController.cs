namespace AllInSkateChallenge.Features.EventDetails;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class EventDetailsController : Controller
{
    private readonly IEventDetailsViewModelBuilder viewModelBuilder;

    private readonly UserManager<ApplicationUser> userManager;

    public EventDetailsController(IEventDetailsViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
    {
        this.viewModelBuilder = viewModelBuilder;
        this.userManager = userManager;
    }

    [Route("FAQ")]
    public async Task<IActionResult> Faq()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("FAQ", "Frequently Asked Questions").WithUser(user).Build();

        return View(model);
    }

    [Route("TermsAndConditions")]
    public async Task<IActionResult> TermsAndConditions()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Terms & Conditions", "Terms & Conditions").WithUser(user).Build();

        return View(model);
    }

    [Authorize]
    [Route("LeaderBoard")]
    public async Task<IActionResult> LeaderBoard()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Leader Board", "Leader Board").WithUser(user).Build();
        model.IsNoIndexPage = true;

        return View(model);
    }

    [Authorize]
    [Route("LatestUpdates")]
    public async Task<IActionResult> LatestUpdates()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Latest Updates", "Latest Updates").WithUser(user).Build();
        model.IsNoIndexPage = true;

        return View(model);
    }
}