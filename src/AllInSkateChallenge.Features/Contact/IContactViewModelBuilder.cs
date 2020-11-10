using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Contact
{
    public interface IContactViewModelBuilder : IPageViewModelBuilder<ContactViewModel>
    {
        IContactViewModelBuilder WithForm(ContactViewModel form);
    }
}
