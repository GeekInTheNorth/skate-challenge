using System.Collections.Generic;

using MediatR;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class GetLongestJourneyLeadersQuery : IRequest<List<SkaterStatisticsModel>>
    {
    }
}
