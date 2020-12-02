using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public interface ILeaderBoardViewModelBuilder : IPageViewModelBuilder<LeaderBoardModel>
    {
        ILeaderBoardViewModelBuilder WithATarget(SkateTarget target);
    }
}
