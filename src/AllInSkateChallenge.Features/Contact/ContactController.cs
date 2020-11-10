using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Contact
{
    public class ContactController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IMediator mediator;

        private readonly IContactViewModelBuilder viewModelBuilder;

        public ContactController(UserManager<ApplicationUser> userManager, IMediator mediator, IContactViewModelBuilder viewModelBuilder)
        {
            this.userManager = userManager;
            this.mediator = mediator;
            this.viewModelBuilder = viewModelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            var model = await BuildModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                var failureModel = await BuildModel(contact);

                return View(failureModel);
            }

            await mediator.Send(contact.ToCommand());

            var successModel = await BuildModel();

            return View("Success", successModel);
        }

        private async Task<PageViewModel<ContactViewModel>> BuildModel(ContactViewModel contact = null)
        {
            if (User != null)
            {
                var user = await userManager.GetUserAsync(User);
                return await viewModelBuilder.WithForm(contact).WithUser(user).Build();
            }

            return await viewModelBuilder.WithForm(contact).Build();
        }
    }
}
