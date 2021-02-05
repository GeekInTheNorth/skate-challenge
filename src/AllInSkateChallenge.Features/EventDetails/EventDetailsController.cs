using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.EventDetails
{
    public class EventDetailsController : Controller
    {
        private readonly IEventDetailsViewModelBuilder viewModelBuilder;

        private readonly UserManager<ApplicationUser> userManager;

        public EventDetailsController(IEventDetailsViewModelBuilder viewModelBuilder, UserManager<ApplicationUser> userManager)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.userManager = userManager;
        }

        [Route("FAQ")]
        public async Task<IActionResult> Faq()
        {
            var user = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithTitles("FAQ", "Frequently Asked Questions").WithUser(user).Build();

            return View(model);
        }

        [Route("TermsAndConditions")]
        public async Task<IActionResult> TermsAndConditions()
        {
            var user = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithTitles("Terms & Conditions", "Terms & Conditions").WithUser(user).Build();

            return View(model);
        }

        [Route("LeaderBoard")]
        public async Task<IActionResult> LeaderBoard()
        {
            var user = await userManager.GetUserAsync(User);
            var model = await viewModelBuilder.WithTitles("Leader Board", "Leader Board").WithUser(user).Build();
            model.IsNoIndexPage = true;

            return View(model);
        }
    }
}
