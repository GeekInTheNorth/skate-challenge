using System;

namespace AllInSkateChallenge.Features.Skater
{
    public interface INewSkaterLogEntry
    {
        string JourneyName { get; }

        DistanceUnit DistanceUnit { get; }

        decimal Distance { get; }

        decimal DistanceMiles { get; }

        decimal DistanceKilometres { get; }

        DateTime? DateSkated { get; }
    }
}
