using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Updates;

using MediatR;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModelBuilder : PageViewModelBuilder<HomePageViewModel>, IHomePageViewModelBuilder
    {
        private readonly IMediator mediator;

        public HomePageViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<HomePageViewModel>> Build()
        {
            var eventStatistics = await mediator.Send(new EventSummaryQuery());
            var model = await base.Build();
            model.PageTitle = "Home";
            model.DisplayPageTitle = "Welcome to the ALL IN Leeds-Liverpool Skate Challenge";
            model.IsNoIndexPage = false;
            model.Content.NumberOfSkaters = eventStatistics.NumberOfSkaters;
            model.Content.CumulativeMiles = eventStatistics.CumulativeMiles;

            if (this.User != null)
            {
                var latestUpdates = await mediator.Send(new LatestUpdatesQuery { Page = 1, PageSize = 10 });

                model.Content.LatestUpdates = latestUpdates.Entries;
            }

            return model;
        }
    }
}
