namespace AllInSkateChallenge.Features.Statistics
{
    using System.Collections.Generic;

    using AllInSkateChallenge.Features.Data.Entities;

    public interface ISkaterTargetAnalyser
    {
        SkaterTargetAnalysis Analyse(ApplicationUser skater, List<SkateLogEntry> allSessions);
    }
}