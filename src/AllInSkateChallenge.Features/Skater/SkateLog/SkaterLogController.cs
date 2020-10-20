using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.SkateLog;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Controllers
{
    [Authorize]
    public class SkaterLogController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ISkaterLogViewModelBuilder viewModelBuilder;

        private readonly IMediator mediator;

        public SkaterLogController(
            UserManager<ApplicationUser> userManager,
            ISkaterLogViewModelBuilder viewModelBuilder,
            IMediator mediator)
        {
            this.userManager = userManager;
            this.viewModelBuilder = viewModelBuilder;
            this.mediator = mediator;
        }

        [Route("skater/skate-log")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            var model = await viewModelBuilder.WithUser(user).Build();

            return View("~/Views/Skater/Log.cshtml", model);
        }

        [HttpPost]
        [Route("skater/skate-log/delete")]
        public async Task<IActionResult> Delete(Guid mileageEntryId)
        {
            var user = await userManager.GetUserAsync(User);

            var command = new DeleteActivityCommand { Skater = user, MileageEntryId = mileageEntryId };
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skater/skate-log")]
        public async Task<IActionResult> Index([FromForm] SkaterLogViewModel mileageEntry)
        {
            var user = await userManager.GetUserAsync(User);

            if (!TryValidateModel(mileageEntry, nameof(SkaterLogViewModel)))
            {
                var model = await viewModelBuilder.WithNewEntry(mileageEntry).WithUser(user).Build();

                return View("~/Views/Skater/Log.cshtml", model);
            }
            else
            {
                var command = new SaveActivityCommand { Skater = user, Distance = mileageEntry.Distance, DistanceUnit = mileageEntry.DistanceUnit, StartDate = DateTime.Now };
                await mediator.Send(command);

                var model = await viewModelBuilder.WithUser(user).Build();

                return View("~/Views/Skater/Log.cshtml", model);
            }
        }
    }
}
