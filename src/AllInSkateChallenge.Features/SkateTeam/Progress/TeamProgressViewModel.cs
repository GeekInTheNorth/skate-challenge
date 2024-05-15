using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;

namespace AllInSkateChallenge.Features.SkateTeam.Progress;

public sealed class TeamProgressViewModel
{
    public List<SkateLogEntry> UserEntries { get; internal set; }

    public List<SkateLogEntry> TeamEntries { get; internal set; }

    public List<CheckPointModel> CheckPointsReached { get; internal set; }

    public SkateTeamModel SkateTeam { get; internal set; }

    public List<string> TeamMembers { get; internal set; }
    
    public CheckPointModel NextCheckPoint { get; internal set; }
    
    public decimal KilometersSkated { get; internal set; }
    
    public decimal TargetKilometers { get; internal set; }
}