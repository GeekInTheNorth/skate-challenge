using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModel
    {
        public decimal MilesSkated { get; set; }

        public decimal TargetMiles { get; set; }

        public List<SkaterProgressCheckPoint> CheckPointsReached { get; set; }

        public SkaterProgressCheckPoint NextCheckPoint { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
