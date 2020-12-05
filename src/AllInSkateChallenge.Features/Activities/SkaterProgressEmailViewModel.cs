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

        public string SponsorLogoUrl { get; set; }

        public decimal TotalMiles { get; set; }

        public decimal TotalMilesRemaining => TotalMiles >= TargetCheckPoint.Distance ? 0 : TargetCheckPoint.Distance - TotalMiles;

        public decimal NextCheckPointMilesRemaining => NextCheckPoint != null ? NextCheckPoint.Distance - TotalMiles : 0;

        public CheckPointModel TargetCheckPoint { get; set; }
    }
}
