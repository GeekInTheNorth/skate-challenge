using AllInSkateChallenge.Features.Data.Static;

using MediatR;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQuery : IRequest<LeaderBoardQueryResponse>
    {
        public int? PageSize { get; set; }

        public SkateTarget Target { get; set; }
    }
}
