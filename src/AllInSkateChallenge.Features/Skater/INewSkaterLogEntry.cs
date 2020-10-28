using System;

namespace AllInSkateChallenge.Features.Skater
{
    public interface INewSkaterLogEntry
    {
        DistanceUnit DistanceUnit { get; }

        decimal Distance { get; }

        decimal DistanceMiles { get; }

        decimal DistanceKilometres { get; }

        DateTime? DateSkated { get; }
    }
}
