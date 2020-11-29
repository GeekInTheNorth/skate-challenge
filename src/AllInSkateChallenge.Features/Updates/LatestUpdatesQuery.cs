using MediatR;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQuery : IRequest<LatestUpdatesQueryResponse>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
