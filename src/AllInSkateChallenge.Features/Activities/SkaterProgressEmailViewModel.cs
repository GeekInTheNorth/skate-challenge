using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Activities
{
    public class SkaterProgressEmailViewModel
    {
        public ApplicationUser Skater { get; set; }

        public CheckPointModel CheckPoint { get; set; }

        public CheckPointModel NextCheckPoint { get; set; }

        public string LogoUrl { get;  set; }

        public decimal TotalMiles { get; set; }

        public decimal TotalMilesRemaining => TotalMiles >= TargetCheckPoint.DistanceInKilometers ? 0 : TargetCheckPoint.DistanceInKilometers - TotalMiles;

        public decimal NextCheckPointMilesRemaining => NextCheckPoint != null ? NextCheckPoint.DistanceInKilometers - TotalMiles : 0;

        public CheckPointModel TargetCheckPoint { get; set; }
    }
}
