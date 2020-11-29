using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewModel
    {
        public List<MileageEntryModel> LatestUpdates { get; set; }
        public int MaxPages { get; set; }
        public int CurrentPage { get; set; }
    }
}