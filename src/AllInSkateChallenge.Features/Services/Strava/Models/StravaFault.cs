using System.Collections.Generic;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Services.Strava.Models
{
    public class StravaFault
    {
        [JsonProperty("errors")]
        public List<StravaError> Errors { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
