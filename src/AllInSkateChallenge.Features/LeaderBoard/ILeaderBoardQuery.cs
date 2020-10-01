namespace AllInSkateChallenge.Features.LeaderBoard
{
    public interface ILeaderBoardQuery
    {
        LeaderBoardModel Get(int size = 10);
    }
}
