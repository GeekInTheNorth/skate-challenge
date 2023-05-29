namespace AllInSkateChallenge.Features.Strava.Models;

using System;

using Newtonsoft.Json;

public class StravaActivity
{
    [JsonProperty("id")]
    public string ActivityId { get; set; }

    public string Name { get; set; }

    public StravaAthlete Athlete { get; set; }

    [JsonProperty("distance")]
    public decimal DistanceMetres { get; set; }

    [JsonProperty("total_elevation_gain")]
    public decimal ElevationGainMetres { get; set; }

    [JsonProperty("elev_low")]
    public decimal LowestElevationMetres { get; set; }

    [JsonProperty("elev_high")]
    public decimal HighestElevationMetres { get; set; }

    [JsonProperty("type")]
    public string ActivityType { get; set; }

    [JsonProperty("workout_type")]
    public string WorkoutType { get; set; }

    [JsonProperty("start_date")]
    public DateTime StartDate { get; set; }

    [JsonProperty("moving_time")]
    public int MovingTime { get; set; }

    [JsonProperty("elapsed_time")]
    public int ElapsedTime { get; set; }

    [JsonProperty("max_speed")]
    public decimal TopSpeed { get; set; }

    [JsonProperty("average_speed")]
    public decimal AverageSpeed { get; set; }
}
