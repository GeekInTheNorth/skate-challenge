using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Strava
{
    [Authorize]
    [ApiController]
    [Route("api/strava/importlatest")]
    public class StravaSynchronisationApiController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly UserManager<ApplicationUser> userManager;

        public StravaSynchronisationApiController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        public async Task<ActionResult<StravaImportResultModel>> ImportLatest()
        {
            var user = await userManager.GetUserAsync(User);
            var queryResponse = await mediator.Send(new StravaPendingImportsQuery { applicationUser = user });
            var responseModel = new StravaImportResultModel();

            if (queryResponse.Activities != null)
            {
                var allowedTypes = new List<string> { "IceSkate", "InlineSkate", "Skateboard" };
                foreach (var activity in queryResponse.Activities)
                {
                    if (allowedTypes.Contains(activity.ActivityType))
                    {
                        await mediator.Send(
                            new SaveActivityCommand 
                            { 
                                Skater = user, 
                                Distance = activity.DistanceMetres, 
                                DistanceUnit = DistanceUnit.Metres, 
                                ElevationGain = activity.ElevationGainMetres, 
                                ElevationGainUnit = DistanceUnit.Metres, 
                                LowestElevation = activity.LowestElevationMetres,
                                LowestElevationUnit = DistanceUnit.Metres,
                                HighestElevation = activity.HighestElevationMetres,
                                HighestElevationUnit = DistanceUnit.Metres,
                                StartDate = activity.StartDate, 
                                StavaActivityId = activity.ActivityId, 
                                TopSpeed = activity.TopSpeed,
                                TopSpeedUnit = VelocityUnit.MetresPerSecond,
                                AverageSpeed = activity.AverageSpeed,
                                AverageSpeedUnit = VelocityUnit.MetresPerSecond,
                                Duration = activity.ElapsedTime,
                                Name = activity.Name 
                            });

                        responseModel.NumberImported++;
                    }
                    else
                    {
                        await mediator.Send(new IgnoreActivitiesCommand { Skater = user, StravaActivityIds = new List<string> { activity.ActivityId } });
                        responseModel.NumberIgnored++;
                    }
                }
            }

            return responseModel;
        }
    }
}
