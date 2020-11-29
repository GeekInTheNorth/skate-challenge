using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewModelBuilder : PageViewModelBuilder<LatestUpdatesViewModel>, ILatestUpdatesViewModelBuilder
    {
        private readonly IMediator mediator;

        private int page;

        private int pageSize;

        public LatestUpdatesViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        public ILatestUpdatesViewModelBuilder WithPaging(int page, int pageSize)
        {
            this.page = page;
            this.pageSize = pageSize;

            return this;
        }

        public override async Task<PageViewModel<LatestUpdatesViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Latest Updates";
            model.DisplayPageTitle = "Latest Updates";
            model.IsNoIndexPage = true;

            var query = new LatestUpdatesQuery { Page = page, PageSize = pageSize };
            var result = await mediator.Send(query);
            model.Content.LatestUpdates = result.Entries;
            model.Content.MaxPages = result.MaxPages;
            model.Content.CurrentPage = result.CurrentPage;

            return model;
        }
    }
}