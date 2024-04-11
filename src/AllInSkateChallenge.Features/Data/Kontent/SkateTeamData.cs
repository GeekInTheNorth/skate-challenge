using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class SkateTeamData
{
    public string TeamName { get; set; }

    public IEnumerable<IAsset> TeamLogo { get; set; }
}