using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava.User;

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
            var user = await userManager.GetStravaDetails(User);

            // Only strava users should be able to get here
            if (!user.IsStravaAuthenticated)
            {
                return Redirect("/");
            }

            var model = await viewModelBuilder.WithUser(user).Build();

            return View("~/Views/Skater/StravaImport.cshtml", model);
        }

        [HttpPost]
        [Route("skater/skate-log/strava-import/save")]
        public async Task<IActionResult> Save(
            string activityId, 
            string name, 
            DateTime logged, 
            decimal miles, 
            decimal lowestElevation,
            decimal highestElevation,
            decimal totalElevation,
            decimal averageSpeed,
            decimal topSpeed,
            int duration)
        {
            var stravaDetails = await userManager.GetStravaDetails(User);
            if (stravaDetails == null || !stravaDetails.IsStravaAuthenticated)
            {
                return Forbid();
            }

            if (string.IsNullOrWhiteSpace(activityId) || miles <= 0)
            {
                return BadRequest();
            }

            var saveCommand = new SaveActivityCommand 
            { 
                Skater = stravaDetails.User, 
                Distance = miles, 
                DistanceUnit = DistanceUnit.Miles, 
                StartDate = logged, 
                StavaActivityId = activityId, 
                Name = name ,
                LowestElevation = lowestElevation,
                LowestElevationUnit = DistanceUnit.Feet,
                HighestElevation = highestElevation,
                HighestElevationUnit = DistanceUnit.Feet,
                ElevationGain = totalElevation,
                ElevationGainUnit = DistanceUnit.Feet,
                AverageSpeed = averageSpeed,
                AverageSpeedUnit = VelocityUnit.MilesPerHour,
                TopSpeed = topSpeed,
                TopSpeedUnit = VelocityUnit.MilesPerHour,
                Duration = duration
            };

            await mediator.Send(saveCommand);

            return Ok();
        }

        [HttpPost]
        [Route("skater/skate-log/strava-import/ignore")]
        public async Task<ActionResult<IgnoreActivitiesCommandResponse>> Ignore(IgnoreActivitiesCommand command)
        {
            var user = await userManager.GetStravaDetails(User);
            if (user == null || !user.IsStravaAuthenticated)
            {
                return Forbid();
            }

            command.Skater = user.User;

            return await mediator.Send(command);
        }
    }
}
