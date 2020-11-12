using MediatR;
using MediatR.Pipeline;

namespace AllInSkateChallenge.Features.Home
{
    public class EventStatisticsQuery : IRequest<EventStatisticsQueryResponse>
    {
    }
}
