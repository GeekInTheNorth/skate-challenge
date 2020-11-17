using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.EventDetails
{
    public class EventDetailsViewModelBuilder : PageViewModelBuilder<EventDetailsViewModel>, IEventDetailsViewModelBuilder
    {
        private string pageTitle;

        private string displayPageTitle;

        public EventDetailsViewModelBuilder(IMediator mediator) : base(mediator)
        {
        }

        public IEventDetailsViewModelBuilder WithTitles(string pageTitle, string displayPageTitle)
        {
            this.pageTitle = pageTitle;
            this.displayPageTitle = displayPageTitle;

            return this;
        }

        public override async Task<PageViewModel<EventDetailsViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = pageTitle;
            model.DisplayPageTitle = displayPageTitle;
            model.IsNoIndexPage = true;

            return model;
        }
    }
}
