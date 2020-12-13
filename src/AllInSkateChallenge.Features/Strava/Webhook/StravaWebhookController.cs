using System;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Strava.Webhook.Deauthorise;
using AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration;
using AllInSkateChallenge.Features.Strava.Webhook.SaveStravaEvent;

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
            if (webHookEvent == null)
            {
                return BadRequest();
            }

            // https://developers.strava.com/docs/webhooks/
            // Log the unprocessed event to assist with understanding data.
            var logCommand = new LogStravaIntegrationCommand { Event = webHookEvent };
            await mediator.Send(logCommand);

            // Assign activity based events to their respective owners
            if (string.Equals("activity", webHookEvent?.ObjectType, StringComparison.CurrentCultureIgnoreCase))
            {
                var newEventCommand = new SaveStravaEventCommand { StravaUserId = webHookEvent.OwnerId, ActivityId = webHookEvent.ObjectId, Updates = webHookEvent.Updates };
                await mediator.Send(newEventCommand);
            }

            var hasDeAuthentication = webHookEvent.Updates.Any(x => x.Key.Equals("authorized", StringComparison.CurrentCultureIgnoreCase) && x.Value.Equals("false", StringComparison.CurrentCultureIgnoreCase));
            if (string.Equals("athlete", webHookEvent?.ObjectType, StringComparison.CurrentCultureIgnoreCase) && hasDeAuthentication)
            {
                var deAuthCommand = new DeauthoriseStravaUserCommand { StravaUserId = webHookEvent.OwnerId };
                await mediator.Send(deAuthCommand);
            }

            return Ok();
        }
    }
}
