namespace AllInSkateChallenge.Features.EventDetails;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.SkateTeam;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public sealed class EventDetailsController(
    IEventDetailsViewModelBuilder viewModelBuilder, 
    UserManager<ApplicationUser> userManager) : Controller
{
    [Route("FAQ")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> Faq()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("FAQ", "Frequently Asked Questions").WithUser(user).Build();

        return View(model);
    }

    [Route("TermsAndConditions")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> TermsAndConditions()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Terms & Conditions", "Terms & Conditions").WithUser(user).Build();

        return View(model);
    }

    [Authorize]
    [Route("LeaderBoard")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> LeaderBoard()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Leader Board", "Leader Board").WithUser(user).Build();
        model.IsNoIndexPage = true;

        return View(model);
    }

    [Authorize]
    [Route("LatestUpdates")]
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> LatestUpdates()
    {
        var user = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithTitles("Latest Updates", "Latest Updates").WithUser(user).Build();
        model.IsNoIndexPage = true;

        return View(model);
    }
}