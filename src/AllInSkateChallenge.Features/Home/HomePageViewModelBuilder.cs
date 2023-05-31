using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

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
            model.DisplayPageTitle = "Welcome to the Roller Girl Gang Virtual Skate Marathon";
            model.IsNoIndexPage = false;
            model.Content.NumberOfSkaters = eventStatistics.NumberOfSkaters;
            model.Content.CumulativeMiles = eventStatistics.CumulativeMiles;

            return model;
        }
    }
}
