using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardViewModelBuilder : PageViewModelBuilder<LeaderBoardModel>, ILeaderBoardViewModelBuilder
    {
        private readonly IMediator mediator;

        public LeaderBoardViewModelBuilder(IMediator mediator, ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<LeaderBoardModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Leader Board";
            model.DisplayPageTitle = "Leader Board";
            
            var leaderBoardResponse = await mediator.Send(new LeaderBoardQuery());
            model.Content.Entries = leaderBoardResponse?.Entries ?? new List<LeaderBoardEntryModel>();

            return model;
        }
    }
}
