using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    public class SubscriptionResponse
    {
        [JsonProperty("hub.challenge")]
        public string HubChallenge { get; set; }
    }
}
