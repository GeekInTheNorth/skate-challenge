using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Strava.Webhook.CreateActivity;
using AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [ApiController]
    public class StravaWebhookController : ControllerBase
    {
        private readonly StravaSettings stravaSettings;

        private readonly IMediator mediator;

        public StravaWebhookController(IOptions<StravaSettings> stravaSettings, IMediator mediator)
        {
            this.stravaSettings = stravaSettings.Value;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("api/strava/event")]
        public IActionResult SubscriptionEvents(
            [FromQuery(Name = "hub.challenge")] string hubChallenge, 
            [FromQuery(Name = "hub.verify_token")] string hubVerificationToken,
            [FromQuery(Name = "hub.mode")] string hubMode)
        {
            if (!hubVerificationToken.Equals(stravaSettings.WebhookSecret, StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(401, "hub.verify_token does not match the expected valye");
            }

            var responseObject = new SubscriptionResponse { HubChallenge = hubChallenge };

            return new JsonResult(responseObject);
        }

        [HttpPost]
        [Route("api/strava/event")]
        public async Task<IActionResult> AthleteEvents(WebHookEvent webHookEvent)
        {
            // https://developers.strava.com/docs/webhooks/

            // Log the unprocessed event to assist with understanding data.
            var logCommand = new LogStravaIntegrationCommand { Event = webHookEvent };
            await mediator.Send(logCommand);

            // Assign activity based events to their respective owners
            if (webHookEvent.ObjectType.Equals("activity", StringComparison.CurrentCultureIgnoreCase))
            {
                var newEventCommand = new CreateActivityEventCommand { StravaUserId = webHookEvent.OwnerId, ActivityId = webHookEvent.ObjectId };
                await mediator.Send(newEventCommand);
            }
            
            return Ok();
        }
    }
}
