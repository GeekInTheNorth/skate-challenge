using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [Authorize]
    public class StravaWebhookAdminController : Controller
    {
        private readonly StravaSettings stravaSettings;

        private readonly IStravaIntegrationLogRepository logRepository;

        public StravaWebhookAdminController(IOptions<StravaSettings> stravaSettings, IStravaIntegrationLogRepository logRepository)
        {
            this.stravaSettings = stravaSettings.Value;
            this.logRepository = logRepository;
        }

        [Route("strava/webhook/admin/{secret}")]
        public async Task<IActionResult> Index(string secret)
        {
            if (!secret.Equals(stravaSettings.WebhookSecret, StringComparison.CurrentCultureIgnoreCase))
            {
                return NotFound();
            }

            var logs = await logRepository.Get(14);

            return View(logs);
        }
    }
}
