using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class SkateTeamData
{
    public string UrlSlug { get; set; }

    public string TeamName { get; set; }

    public IEnumerable<IAsset> TeamLogo { get; set; }

    public string TeamDescriptionPlain { get; set; }

    public string TeamDescriptionRich { get; set; }
}