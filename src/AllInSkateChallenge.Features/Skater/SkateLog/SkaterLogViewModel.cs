using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public partial class SkaterLogViewModel
    {
        public bool IsStravaAccount { get; set; }

        public decimal TotalMiles { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
