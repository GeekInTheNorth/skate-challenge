using System.Collections.Generic;

using AllInSkateChallenge.Features.Services.Strava.Models;

namespace AllInSkateChallenge.Features.Services.Strava
{
    public class StravaActivityListResponse
    {
        public List<StravaActivity> Activities { get; set; }

        public StravaFault Faults { get; set; }

        public bool IsError => Activities == null || Faults != null;
    }
}
