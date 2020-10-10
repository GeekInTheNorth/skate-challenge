using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Models
{
    public class StravaError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }
    }
}
