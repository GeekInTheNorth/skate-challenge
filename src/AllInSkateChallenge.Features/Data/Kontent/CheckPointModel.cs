using System.Collections.Generic;
using System.Linq;

using AllInSkateChallenge.Features.Common;

namespace AllInSkateChallenge.Features.Data.Kontent;

public class CheckPointModel
{
    public int SkateTarget { get; set; }

    public decimal DistanceInKilometers { get; set; }

    public decimal DistanceInMiles => Conversion.KilometresToMiles(DistanceInKilometers);

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public List<CheckPointUrl> Links { get; set; } = new();

    public string Image { get; set; }

    public List<string> AllImages { get; set; } = new();

    public string DigitalBadge { get; set; }

    public string DigitalBadgeName => DigitalBadge?.Split('/').Last();

    public string FinisherDigitalBadge { get; set; }

    public string FinisherDigitalBadgeName => FinisherDigitalBadge?.Split('/').Last();

    public bool IsEndPoint { get; set; }
}

public class CheckPointUrl
{
    public string Title { get; set; }

    public string Url { get; set; }
}