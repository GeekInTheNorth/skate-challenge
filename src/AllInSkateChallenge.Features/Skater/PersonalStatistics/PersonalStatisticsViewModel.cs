namespace AllInSkateChallenge.Features.Skater.PersonalStatistics;

using AllInSkateChallenge.Features.Common;

public class PersonalStatisticsViewModel
{
    public decimal LongestDistanceMiles { get; set; }

    public decimal LongestDistanceKilometres => Conversion.MilesToKilometres(LongestDistanceMiles);

    public decimal ShortestDistanceMiles { get; set; }

    public decimal ShortestDistanceKilometres => Conversion.MilesToKilometres(ShortestDistanceMiles);

    public decimal TotalDistanceMiles { get; set; }

    public decimal TotalDistanceKilometres => Conversion.MilesToKilometres(TotalDistanceMiles);

    public bool ShowDistance => LongestDistanceMiles != 0 || ShortestDistanceMiles != 0 || TotalDistanceMiles != 0;

    public decimal BestAverageSpeedMph { get; set; }

    public decimal BestAverageSpeedKph => Conversion.MilesPerHourToKilometresPerHour(BestAverageSpeedMph);

    public decimal BestTopSpeedMph { get; set; }

    public decimal BestTopSpeedKph => Conversion.MilesPerHourToKilometresPerHour(BestTopSpeedMph);

    public decimal NumberOfSessions { get; set; }

    public bool ShowSpeed => BestAverageSpeedMph != 0 || BestTopSpeedMph != 0;

    public decimal HighestElevationFeet { get; set; }

    public decimal HighestElevationMetres => Conversion.FeetToMetres(HighestElevationFeet);

    public decimal LowestElevationFeet { get; set; }

    public decimal LowestElevationMetres => Conversion.FeetToMetres(LowestElevationFeet);

    public decimal GreatestElevationGainFeet { get; set; }

    public decimal GreatestElevationGainMetres => Conversion.FeetToMetres(GreatestElevationGainFeet);

    public bool ShowElevations => HighestElevationFeet != 0 || LowestElevationFeet != 0 || GreatestElevationGainFeet != 0;

    public bool ShowPersonalStatistics => ShowDistance || ShowSpeed || ShowElevations;
}