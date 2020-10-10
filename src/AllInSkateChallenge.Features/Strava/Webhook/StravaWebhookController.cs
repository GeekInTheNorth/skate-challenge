using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [ApiController]
    public class StravaWebhookController : ControllerBase
    {
        private StravaSettings stravaSettings;

        public StravaWebhookController(IOptions<StravaSettings> stravaSettings)
        {
            this.stravaSettings = stravaSettings.Value;
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
        public IActionResult AthleteEvents(WebHookEvent webHookEvent)
        {
            // https://developers.strava.com/docs/webhooks/
            // Aim for asyncronous activity which may be fragmented across subscriptions

            return Ok();
        }
    }
}
