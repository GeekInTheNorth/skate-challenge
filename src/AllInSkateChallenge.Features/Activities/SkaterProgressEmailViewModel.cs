namespace AllInSkateChallenge.Features.Activities;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;

public class SkaterProgressEmailViewModel
{
    public ApplicationUser Skater { get; set; }

    public CheckPointModel CheckPoint { get; set; }

    public CheckPointModel NextCheckPoint { get; set; }

    public string LogoUrl { get;  set; }

    public decimal TotalKilometres { get; set; }

    public decimal TotalKilometresRemaining => TotalKilometres >= TargetCheckPoint.DistanceInKilometers ? 0 : TargetCheckPoint.DistanceInKilometers - TotalKilometres;

    public decimal NextCheckPointKilometresRemaining => NextCheckPoint != null ? NextCheckPoint.DistanceInKilometers - TotalKilometres : 0;

    public CheckPointModel TargetCheckPoint { get; set; }
}