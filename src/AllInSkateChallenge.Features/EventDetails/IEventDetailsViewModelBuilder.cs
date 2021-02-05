using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.EventDetails
{
    public interface IEventDetailsViewModelBuilder : IPageViewModelBuilder<EventDetailsViewModel>
    {
        IEventDetailsViewModelBuilder WithTitles(string pageTitle, string displayPageTitle);

        IEventDetailsViewModelBuilder WithNoIndex();
    }
}
