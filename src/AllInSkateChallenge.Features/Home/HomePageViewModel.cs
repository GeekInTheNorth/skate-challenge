using System.Collections.Generic;

using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModel
    {
        public bool ShowSignUpPromotion { get; internal set; }

        public List<LeaderBoardEntryModel> LeaderBoard { get; internal set; }

        public List<MileageEntryModel> LatestUpdates { get; internal set; }

        public SummaryStatisticsModel SummaryStatistics { get; internal set; }
    }
}
