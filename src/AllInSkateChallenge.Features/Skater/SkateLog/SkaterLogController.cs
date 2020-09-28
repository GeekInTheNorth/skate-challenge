using System;
using System.Threading.Tasks;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;
using AllInSkateChallenge.Features.Skater.SkateLog;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Controllers
{
    [Authorize]
    public class SkaterLogController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ISkaterMileageEntriesRepository repository;

        private readonly ISkaterLogViewModelBuilder viewModelBuilder;

        public SkaterLogController(
            UserManager<ApplicationUser> userManager, 
            ISkaterMileageEntriesRepository repository, 
            ISkaterLogViewModelBuilder viewModelBuilder)
        {
            this.userManager = userManager;
            this.repository = repository;
            this.viewModelBuilder = viewModelBuilder;
        }

        [Route("skater/skate-log")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            var model = viewModelBuilder.WithUser(user).Build();

            return View("~/Views/Skater/Log.cshtml", model);
        }

        [HttpPost]
        [Route("skater/skate-log")]
        [Route("skater/skate-log/delete")]
        public async Task<IActionResult> Index(int mileageEntryId)
        {
            var user = await userManager.GetUserAsync(User);
            var deleteState = SkateLogDeleteState.None;

            try
            {
                await repository.DeleteAsync(mileageEntryId);
                deleteState = SkateLogDeleteState.Success;
            }
            catch (Exception)
            {
                deleteState = SkateLogDeleteState.Success;
            }

            var model = viewModelBuilder.WithUser(user).Build();
            model.DeleteState = deleteState;

            return View("~/Views/Skater/Log.cshtml", model);
        }
    }
}
