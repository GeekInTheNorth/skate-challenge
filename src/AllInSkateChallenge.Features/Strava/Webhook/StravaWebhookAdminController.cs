using System.Threading.Tasks;

using AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [Authorize(Roles = "Administrator")]
    public class StravaWebhookAdminController : Controller
    {
        private readonly IMediator mediator;

        public StravaWebhookAdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("strava/webhook/admin")]
        public async Task<IActionResult> Index()
        {
            var query = new StravaLogQuery { NumberOfDays = 14 };
            var response = await mediator.Send(query);

            return View(response.Logs);
        }
    }
}
