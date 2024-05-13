using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog;

public class SkaterLogQueryResponse
{
    public List<SkateLogEntry> Entries { get; internal set; }

    public List<SkateLogEntry> TeamEntries { get; internal set; }
}