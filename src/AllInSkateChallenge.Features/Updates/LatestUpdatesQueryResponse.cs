using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQueryResponse
    {
        public int TotalUpdates { get; set; }

        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }

        public List<MileageEntryModel> Entries { get; set; }
    }
}
