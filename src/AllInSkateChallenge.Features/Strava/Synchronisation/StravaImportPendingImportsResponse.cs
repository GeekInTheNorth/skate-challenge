using System.Collections.Generic;

using AllInSkateChallenge.Features.Strava.Models;

namespace AllInSkateChallenge.Features.Strava
{
    public class StravaImportPendingImportsResponse
    {
        public List<StravaActivity> Activities { get; set; }
    }
}
