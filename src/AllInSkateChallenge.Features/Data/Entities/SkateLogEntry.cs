namespace AllInSkateChallenge.Features.Data.Entities;

using System;
using System.ComponentModel.DataAnnotations.Schema;

using AllInSkateChallenge.Features.Common;

public class SkateLogEntry
{
    public Guid SkateLogEntryId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public string ApplicationUserId { get; set; }

    [Column(TypeName = "decimal(18, 6)")]
    public decimal DistanceInMiles { get; set; }

    public decimal DistanceInKilometres => Conversion.MilesToKilometres(DistanceInMiles);

    [Column(TypeName = "decimal(18, 6)")]
    public decimal ElevationGainInFeet { get; set; }

    public decimal ElevationGainInMetres => Conversion.FeetToMetres(ElevationGainInFeet);

    [Column(TypeName = "decimal(18, 6)")]
    public decimal LowestElevationInFeet { get; set; }

    public decimal LowestElevationInMetres => Conversion.FeetToMetres(LowestElevationInFeet);

    [Column(TypeName = "decimal(18, 6)")]
    public decimal HighestElevationInFeet { get; set; }

    public decimal HighestElevationInMetres => Conversion.FeetToMetres(HighestElevationInFeet);

    [Column(TypeName = "decimal(18, 6)")]
    public decimal AverageSpeedInMph { get; set; }

    public decimal AverageSpeedInKph => Conversion.MilesPerHourToKilometresPerHour(AverageSpeedInMph);

    [Column(TypeName = "decimal(18, 6)")]
    public decimal TopSpeedInMph { get; set; }

    public decimal TopSpeedInKph => Conversion.MilesPerHourToKilometresPerHour(TopSpeedInMph);

    public DateTime Logged { get; set; }

    public int Duration { get; set; }

    public string StravaId { get; set; }

    public string Name { get; set; }

    public bool IsMultipleEntry { get; set; }
}