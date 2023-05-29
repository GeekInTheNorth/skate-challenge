namespace AllInSkateChallenge.Features.Strava.Webhook;

using System.Collections.Generic;

using Newtonsoft.Json;

public class WebHookEvent 
{ 
    [JsonProperty("aspect_type")]
    public string AspectType { get; set; }

    [JsonProperty("event_time")]
    public long EventTime { get; set; }

    [JsonProperty("object_id")]
    public string ObjectId { get; set; }

    [JsonProperty("object_type")]
    public string ObjectType { get; set; }

    [JsonProperty("owner_id")]
    public string OwnerId { get; set; }

    [JsonProperty("subscription_id")]
    public string SubscriptionId { get; set; }

    [JsonProperty("updates")]
    public Dictionary<string, string> Updates { get; set; }
}
