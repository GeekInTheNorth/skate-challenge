using AllInSkateChallenge.Features.LeaderBoard;
using AllInSkateChallenge.Features.Updates;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModel
    {
        public LeaderBoardModel LeaderBoard { get; internal set; }

        public MileageUpdateModel LatestUpdates { get; internal set; }
    }
}
