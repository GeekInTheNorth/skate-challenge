using System;

namespace AllInSkateChallenge.Features.Skater
{
    public interface INewSkaterLogEntry
    {
        string JourneyName { get; }

        DistanceUnit DistanceUnit { get; }

        decimal Distance { get; }

        DateTime? DateSkated { get; }

        VelocityUnit VelocityUnit { get; }

        decimal AverageSpeed { get; }

        decimal TopSpeed { get; }

        DistanceUnit ElevationUnit { get; }

        decimal LowestElevation { get; }

        decimal HighestElevation { get; }

        decimal ElevationGain { get; }
    }
}
