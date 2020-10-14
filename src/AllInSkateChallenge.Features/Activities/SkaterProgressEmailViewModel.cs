using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Activities
{
    public class SkaterProgressEmailViewModel
    {
        public ApplicationUser Skater { get; set; }

        public CheckPointModel CheckPoint { get; set; }

        public string LogoUrl { get;  set; }

        public decimal TotalMiles { get; set; }
    }
}
