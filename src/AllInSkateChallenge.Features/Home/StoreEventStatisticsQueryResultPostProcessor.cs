using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR.Pipeline;

namespace AllInSkateChallenge.Features.Home
{
    public class StoreEventStatisticsQueryResultPostProcessor : IRequestPostProcessor<EventStatisticsQuery, EventStatisticsQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public StoreEventStatisticsQueryResultPostProcessor(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Process(EventStatisticsQuery request, EventStatisticsQueryResponse response, CancellationToken cancellationToken)
        {
            if (!response.StatisticsExists)
            {
                var statisticStore = new EventStatistic { NumberOfSkaters = response.NumberOfSkaters, CumulativeMiles = response.CumulativeMiles };

                context.EventStatistics.Add(statisticStore);
                await context.SaveChangesAsync();
            }
        }
    }
}
