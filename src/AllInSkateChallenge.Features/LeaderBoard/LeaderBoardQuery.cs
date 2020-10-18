using MediatR;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardQuery : IRequest<LeaderBoardQueryResponse>
    {
        public int? Limit { get; set; }
    }
}
