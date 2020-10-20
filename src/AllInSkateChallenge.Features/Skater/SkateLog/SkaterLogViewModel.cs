using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public partial class SkaterLogViewModel
    {
        public decimal TotalMiles { get; set; }

        public List<SkateLogEntry> Entries { get; set; }
    }
}
