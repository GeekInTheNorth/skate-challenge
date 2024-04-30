using System.Threading.Tasks;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Home;

public class HomePageViewModelBuilder(IMediator mediator) : PageViewModelBuilder<HomePageViewModel>(mediator), IHomePageViewModelBuilder
{
    private readonly IMediator mediator = mediator;
    
    public override async Task<PageViewModel<HomePageViewModel>> Build()
    {
        var eventStatistics = await mediator.Send(new EventSummaryQuery());
        var model = await base.Build();
        model.PageTitle = "Home";
        model.DisplayPageTitle = "Welcome to the Roller Girl Gang Virtual Skate Marathon";
        model.IsNoIndexPage = false;
        model.Content.NumberOfSkaters = eventStatistics.NumberOfSkaters;
        model.Content.CumulativeMiles = eventStatistics.CumulativeMiles;
        model.Content.ShowLeaderBoardButton = false;
        model.Content.ShowAllUpdatesButton = false;

        return model;
    }
}
