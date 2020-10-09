using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Webhook
{
    [ApiController]
    public class StravaWebhookController : ControllerBase
    {
        [HttpPost]
        [Route("api/strava/process-event")]
        public IActionResult ProcessEvent(WebHookEvent webHookEvent)
        {
            return Ok();
        }
    }

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
}
