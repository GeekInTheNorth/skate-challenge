using System;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Models
{
    public class StravaActivity
    {
        [JsonProperty("id")]
        public string ActivityId { get; set; }

        public string Name { get; set; }

        public StravaAthlete Athlete { get; set; }

        [JsonProperty("distance")]
        public decimal DistanceMetres { get; set; }

        [JsonProperty("total_elevation_gain")]
        public decimal TotalElevationGainMetres { get; set; }

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
    }
}
