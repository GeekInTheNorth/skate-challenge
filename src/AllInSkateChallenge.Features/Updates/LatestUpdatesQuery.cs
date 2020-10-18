using MediatR;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQuery : IRequest<LatestUpdatesQueryResponse>
    {
        public int Limit { get; set; }
    }
}
