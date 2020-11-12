using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Home
{
    public class EventStatisticsQueryHandler : IRequestHandler<EventStatisticsQuery, EventStatisticsQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public EventStatisticsQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EventStatisticsQueryResponse> Handle(EventStatisticsQuery request, CancellationToken cancellationToken)
        {
            var eventStatistics = await context.EventStatistics.FirstOrDefaultAsync();
            if (eventStatistics != null)
            {
                return new EventStatisticsQueryResponse { StatisticsExists = true, NumberOfSkaters = eventStatistics.NumberOfSkaters, CumulativeMiles = eventStatistics.CumulativeMiles };
            }

            var skaterMiles = (from entries in context.SkateLogEntries
                               group entries by entries.ApplicationUserId into userEntries
                               select new
                               {
                                   UserId = userEntries.Key,
                                   TotalMiles = userEntries.Sum(x => x.DistanceInMiles),
                               }).ToList();

            return new EventStatisticsQueryResponse
            {
                StatisticsExists = false,
                NumberOfSkaters = skaterMiles.Count,
                CumulativeMiles = skaterMiles.Sum(x => x.TotalMiles)
            };
        }
    }
}
