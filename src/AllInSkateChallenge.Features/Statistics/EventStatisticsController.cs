namespace AllInSkateChallenge.Features.Statistics
{
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data.Entities;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class EventStatisticsController : Controller
    {
        private readonly IEventStatisticsViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public EventStatisticsController(IEventStatisticsViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userDetails = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithUser(userDetails).Build();

            return View("~/Views/EventStatistics/Index.cshtml", model);
        }

        public async Task<IActionResult> PreviousMonth()
        {
            var userDetails = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithPeriodRange(PeriodRange.PreviousMonth).WithUser(userDetails).Build();

            return View("~/Views/EventStatistics/Index.cshtml", model);
        }

        public async Task<IActionResult> ThisMonth()
        {
            var userDetails = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithPeriodRange(PeriodRange.CurrentMonth).WithUser(userDetails).Build();

            return View("~/Views/EventStatistics/Index.cshtml", model);
        }
    }
}