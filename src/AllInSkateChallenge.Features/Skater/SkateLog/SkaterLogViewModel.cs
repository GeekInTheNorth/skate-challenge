using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogViewModel
    {
        public decimal TotalMiles { get; set; }

        public List<MileageEntry> Entries { get; set; }

        public SkateLogDeleteState DeleteState { get; set; }
    }

    public enum SkateLogDeleteState
    {
        None,
        Failed,
        Success
    }
}
