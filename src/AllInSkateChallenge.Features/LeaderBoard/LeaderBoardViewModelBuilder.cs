using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardViewModelBuilder : PageViewModelBuilder<LeaderBoardModel>, ILeaderBoardViewModelBuilder
    {
        private readonly IMediator mediator;

        private readonly ICheckPointRepository checkPointRepository;

        private SkateTarget target;

        public LeaderBoardViewModelBuilder(IMediator mediator, ICheckPointRepository checkPointRepository) : base(mediator)
        {
            this.mediator = mediator;
            this.checkPointRepository = checkPointRepository;

            target = SkateTarget.LiverpoolCanningDock;
        }

        public ILeaderBoardViewModelBuilder WithATarget(SkateTarget target)
        {
            this.target = target;

            return this;
        }

        public override async Task<PageViewModel<LeaderBoardModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Leader Board";
            model.DisplayPageTitle = "Leader Board";
            model.IsNoIndexPage = true;

            var leaderBoardResponse = await mediator.Send(new LeaderBoardQuery { Target = target });
            model.Content.Entries = leaderBoardResponse?.Entries ?? new List<LeaderBoardEntryModel>();
            model.Content.Targets = checkPointRepository.GetSelectList();
            model.Content.SelectedTarget = target;

            return model;
        }
    }
}
