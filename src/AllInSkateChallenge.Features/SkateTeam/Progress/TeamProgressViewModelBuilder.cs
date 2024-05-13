using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Skater.SkateLog;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.SkateTeam.Progress;

public sealed class TeamProgressViewModelBuilder(ICheckPointRepository checkPointRepository, ISkateTeamRepository skateTeamRepository, IMediator mediator, UserManager<ApplicationUser> userManager)
    : PageViewModelBuilder<TeamProgressViewModel>(mediator), ITeamProgressViewModelBuilder
{
    private readonly ICheckPointRepository checkPointRepository = checkPointRepository;

    private readonly ISkateTeamRepository skateTeamRepository = skateTeamRepository;

    private readonly UserManager<ApplicationUser> userManager = userManager;

    private readonly IMediator mediator = mediator;

    public override async Task<PageViewModel<TeamProgressViewModel>> Build()
    {
        var command = new SkaterLogQuery { Skater = User, IncludeTeam = true };
        var commandResponse = await mediator.Send(command);

        var model = await base.Build();
        model.PageTitle = "Team Progress";
        model.DisplayPageTitle = "Team Progress";
        model.IsNoIndexPage = true;
        model.Content.UserEntries = commandResponse.Entries ?? [];
        model.Content.TeamEntries = commandResponse.TeamEntries ?? [];
        model.Content.CheckpointsReached = checkPointRepository.GetGoalCheckpoints();

        var teams = await skateTeamRepository.GetAsync();
        var skatersTeam = teams.FirstOrDefault(x => x.Id == User.Team);
        if (skatersTeam != null)
        {
            model.PageTitle = $"{skatersTeam.Name}";
            model.DisplayPageTitle = $"{skatersTeam.Name}";
            model.Content.SkateTeam = skatersTeam;
        }

        model.Content.TeamMembers = await userManager.Users.Where(x => x.Team == User.Team && x.HasPaid).OrderBy(x => x.SkaterName).Select(x => x.SkaterName).ToListAsync();

        return model;
    }
}