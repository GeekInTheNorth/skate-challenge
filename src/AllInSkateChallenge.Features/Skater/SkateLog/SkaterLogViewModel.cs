using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public partial class SkaterLogViewModel
    {
        public List<SkateLogEntry> Entries { get; set; }

        public bool RecordExists { get; set; }
    }
}
