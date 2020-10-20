using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    [Authorize]
    public class StravaImportController : Controller
    {
        private readonly IStravaImportViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IMediator mediator;

        public StravaImportController(
            IStravaImportViewModelBuilder viewModelBuilder,
            UserManager<ApplicationUser> userManager,
            IMediator mediator)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [Route("skater/skate-log/strava-import")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            // Only strava users should be able to get here
            if (!user.IsStravaAccount)
            {
                return Redirect("/");
            }

            var model = await viewModelBuilder.WithUser(user).Build();

            return View("~/Views/Skater/StravaImport.cshtml", model);
        }

        [HttpPost]
        [Route("skater/skate-log/strava-import/save")]
        public async Task<IActionResult> Save(string activityId, DateTime logged, decimal miles)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null || !user.IsStravaAccount)
            {
                return Forbid();
            }

            if (string.IsNullOrWhiteSpace(activityId) || miles <= 0)
            {
                return BadRequest();
            }

            var saveCommand = new SaveActivityCommand { Skater = user, Distance = miles, DistanceUnit = DistanceUnit.Miles, StartDate = logged, StavaActivityId = activityId };
            await mediator.Send(saveCommand);

            return Ok();
        }
    }
}
