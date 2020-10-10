using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [ApiController]
    public class StravaWebhookController : ControllerBase
    {
        private readonly StravaSettings stravaSettings;

        private readonly IStravaIntegrationLogRepository logRepository;

        public StravaWebhookController(IOptions<StravaSettings> stravaSettings, IStravaIntegrationLogRepository logRepository)
        {
            this.stravaSettings = stravaSettings.Value;
            this.logRepository = logRepository;
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
            await logRepository.Log(Request.QueryString.Value, webHookEvent);

            return Ok();
        }
    }
}
