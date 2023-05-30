using System.Collections.Generic;

using AllInSkateChallenge.Features.Common;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModel
    {
        public decimal MilesSkated => Conversion.KilometresToMiles(KilometersSkated);

        public decimal TargetMiles => Conversion.KilometresToMiles(TargetKilometers);

        public decimal KilometersSkated { get; set; }

        public decimal TargetKilometers { get; set; }

        public List<SkaterProgressCheckPoint> CheckPointsReached { get; set; }

        public SkaterProgressCheckPoint NextCheckPoint { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
