using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModel
    {
        public bool ShowSignUpPromotion { get; internal set; }

        public LeaderBoardModel LeaderBoard { get; internal set; }

        public MileageUpdateModel LatestUpdates { get; internal set; }

        public SummaryStatisticsModel SummaryStatistics { get; internal set; }
    }
}
