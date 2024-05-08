using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Skater.SkateLog;

using MediatR;

namespace AllInSkateChallenge.Features.SkateTeam.Progress;

public sealed class TeamProgressViewModelBuilder(ICheckPointRepository checkPointRepository, ISkateTeamRepository skateTeamRepository, IMediator mediator) 
    : PageViewModelBuilder<TeamProgressViewModel>(mediator), ITeamProgressViewModelBuilder
{
    private readonly ICheckPointRepository checkPointRepository = checkPointRepository;

    private readonly ISkateTeamRepository skateTeamRepository = skateTeamRepository;

    private readonly IMediator mediator = mediator;

    public override async Task<PageViewModel<TeamProgressViewModel>> Build()
    {
        var command = new SkaterLogQuery { Skater = User };
        var commandResponse = await mediator.Send(command);
        var skateLogEntries = commandResponse.Entries ?? new List<SkateLogEntry>();

        var model = await base.Build();
        model.PageTitle = "Team Progress";
        model.DisplayPageTitle = "Team Progress";
        model.IsNoIndexPage = true;
        model.Content.UserEntries = skateLogEntries;
        model.Content.TeamEntries = skateLogEntries;
        model.Content.CheckpointsReached = checkPointRepository.GetGoalCheckpoints();

        var teams = await skateTeamRepository.GetAsync();
        var skatersTeam = teams.FirstOrDefault(x => x.Id == User.Team);
        if (skatersTeam != null)
        {
            model.PageTitle = $"{skatersTeam.Name}'s Progress";
            model.DisplayPageTitle = $"{skatersTeam.Name}'s Progress";
            model.Content.SkateTeam = skatersTeam;
        }

        return model;
    }
}