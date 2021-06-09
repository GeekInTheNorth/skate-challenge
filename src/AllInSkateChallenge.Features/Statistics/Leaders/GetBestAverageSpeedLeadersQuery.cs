using System.Collections.Generic;

using MediatR;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class GetBestAverageSpeedLeadersQuery : IRequest<List<SkaterStatisticsModel>>
    {
    }
}
