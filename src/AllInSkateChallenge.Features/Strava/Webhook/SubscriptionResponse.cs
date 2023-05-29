namespace AllInSkateChallenge.Features.Strava.Webhook;

using Newtonsoft.Json;

public class SubscriptionResponse
{
    [JsonProperty("hub.challenge")]
    public string HubChallenge { get; set; }
}
