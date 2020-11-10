using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Contact
{
    public class ContactViewModelBuilder : PageViewModelBuilder<ContactViewModel>, IContactViewModelBuilder
    {
        private ContactViewModel formSubmission;

        public ContactViewModelBuilder(IMediator mediator) : base(mediator)
        {
        }

        public IContactViewModelBuilder WithForm(ContactViewModel formSubmission)
        {
            this.formSubmission = formSubmission;

            return this;
        }

        public override async Task<PageViewModel<ContactViewModel>> Build()
        {
            var model = await base.Build();

            model.Content.Email = formSubmission?.Email ?? User?.Email;
            model.Content.Name = formSubmission?.Name;
            model.Content.Message = formSubmission?.Message;
            model.Content.Reason = formSubmission?.Reason;
            model.DisplayPageTitle = "Contact Us";

            return model;
        }
    }
}
