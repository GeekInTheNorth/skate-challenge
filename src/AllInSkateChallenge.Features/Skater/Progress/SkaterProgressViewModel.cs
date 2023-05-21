using System.Collections.Generic;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModel
    {
        public decimal MilesSkated { get; set; }

        public decimal TargetMiles { get; set; }

        public decimal KilometersSkated => Conversion.MilesToKilometres(MilesSkated);

        public decimal TargetKilometers => Conversion.MilesToKilometres(TargetMiles);

        public List<SkaterProgressCheckPoint> CheckPointsReached { get; set; }

        public SkaterProgressCheckPoint NextCheckPoint { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
