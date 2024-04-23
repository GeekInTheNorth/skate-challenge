namespace AllInSkateChallenge.Features.Statistics;

using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.SkateTeam;
using AllInSkateChallenge.Features.Strava.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public sealed class EventStatisticsController(
    IEventStatisticsViewModelBuilder viewModelBuilder, 
    UserManager<ApplicationUser> userManager) : Controller
{
    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> Index()
    {
        var userDetails = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithUser(userDetails).Build();

        return View("~/Views/EventStatistics/Index.cshtml", model);
    }

    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> PreviousMonth()
    {
        var userDetails = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithPeriodRange(PeriodRange.PreviousMonth).WithUser(userDetails).Build();

        return View("~/Views/EventStatistics/Index.cshtml", model);
    }

    [ServiceFilter(typeof(SkateTeamActionFilter))]
    public async Task<IActionResult> ThisMonth()
    {
        var userDetails = await userManager.GetStravaDetails(User);
        var model = await viewModelBuilder.WithPeriodRange(PeriodRange.CurrentMonth).WithUser(userDetails).Build();

        return View("~/Views/EventStatistics/Index.cshtml", model);
    }
}