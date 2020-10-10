using System.Collections.Generic;

using AllInSkateChallenge.Features.Strava.Models;

namespace AllInSkateChallenge.Features.Strava
{
    public class StravaActivityListResponse
    {
        public List<StravaActivity> Activities { get; set; }

        public StravaFault Faults { get; set; }

        public bool IsError => Activities == null || Faults != null;
    }
}
