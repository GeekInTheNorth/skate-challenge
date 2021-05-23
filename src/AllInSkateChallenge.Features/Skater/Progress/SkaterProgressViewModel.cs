using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModel
    {
        public decimal MilesSkated { get; set; }

        public decimal TargetMiles { get; set; }

        public List<CheckPointModel> CheckPointsReached { get; set; }

        public CheckPointModel NextCheckPoint { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
