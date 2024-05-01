using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Home;

public class HomePageViewModelBuilder(IMediator mediator, IHomePageRepository homePageRepository) : PageViewModelBuilder<HomePageViewModel>(mediator), IHomePageViewModelBuilder
{
    private readonly IMediator mediator = mediator;
    private readonly IHomePageRepository homePageRepository = homePageRepository;

    public override async Task<PageViewModel<HomePageViewModel>> Build()
    {
        var eventStatistics = await mediator.Send(new EventSummaryQuery());
        var homePage = await homePageRepository.GetAsync();

        var model = await base.Build();
        model.PageTitle = "Home";
        model.DisplayPageTitle = homePage.Title;
        model.IsNoIndexPage = false;
        model.Content.Introduction = homePage.Introduction;
        model.Content.RegistrationTitle = homePage.RegistrationTitle;
        model.Content.RegistrationGuidance = homePage.RegistrationGuidance;
        model.Content.EventMap = homePage.EventMap;
        model.Content.NumberOfSkaters = eventStatistics.NumberOfSkaters;
        model.Content.CumulativeMiles = eventStatistics.CumulativeMiles;
        model.Content.ShowLeaderBoardButton = false;
        model.Content.ShowAllUpdatesButton = false;

        return model;
    }
}
