using System.Collections.Generic;

using MediatR;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class StatisticLeaderQuery : IRequest<IEnumerable<SkaterStatisticsModel>>
    {
        public StatisticType StatisticType { get; set; }
    }
}
