﻿using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.LeaderBoard;
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
            var eventStatistics = await mediator.Send(new EventStatisticsQuery());
            var model = await base.Build();
            model.PageTitle = "Home";
            model.DisplayPageTitle = "Welcome to the ALL IN Leeds-Liverpool Skate Challenge";
            model.IsNoIndexPage = false;
            model.Content.NumberOfSkaters = eventStatistics.NumberOfSkaters;
            model.Content.CumulativeMiles = eventStatistics.CumulativeMiles;

            if (model.IsLoggedIn)
            {
                var latestUpdates = await mediator.Send(new LatestUpdatesQuery { Page = 1, PageSize = 10 });
                var leaderBoard = await mediator.Send(new LeaderBoardQuery { PageSize = 10, Target = SkateTarget.LiverpoolCanningDock });

                model.Content.LeaderBoard = leaderBoard.Entries;
                model.Content.LatestUpdates = latestUpdates.Entries;
            }

            return model;
        }
    }
}
