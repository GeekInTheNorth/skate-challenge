using System.Collections.Generic;

using AllInSkateChallenge.Features.Services.Strava.Models;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportViewModel
    {
        public List<StravaImportActivityViewModel> Activities { get; set; }

        public StravaFault Fault { get; internal set; }
    }
}
