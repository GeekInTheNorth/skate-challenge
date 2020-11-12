using System.Collections.Generic;

using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModel
    {
        public List<LeaderBoardEntryModel> LeaderBoard { get; set; }

        public List<MileageEntryModel> LatestUpdates { get; set; }

        public int NumberOfSkaters { get; set; }
        
        public decimal CumulativeMiles { get; set; }
    }
}
