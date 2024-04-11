using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class CheckpointData
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IHtmlContent CheckpointReachedEmailContent { get; set; }

    public decimal DistanceFromStart { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public IEnumerable<IAsset> Image { get; set; }

    public IEnumerable<IAsset> DigitalBadge { get; set; }

    public string LocationUrl { get; set; }

    public string LocationUrlTitle { get; set; }

    public string CommunityUrlOne { get; set; }

    public string CommunityUrlOneTitle { get; set; }

    public string CommunityUrlTwo { get; set; }

    public string CommunityUrlTwoTitle { get; set; }

    public string CommunityUrlThree { get; set; }

    public string CommunityUrlThreeTitle { get; set; }

    public IEnumerable<IMultipleChoiceOption> IsPersonalTarget { get; set; }
}