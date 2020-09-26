using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModel
    {
        public string Title { get; set; }

        public decimal MilesSkated { get; set; }
        
        public List<CheckPointModel> CheckPointsReached { get; set; }

        public string NextCheckPointDescription { get; set; }
    }
}
