namespace AllInSkateChallenge.Features.Home
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    public class EventSummaryQueryHandler : IRequestHandler<EventSummaryQuery, EventSummaryQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public EventSummaryQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EventSummaryQueryResponse> Handle(EventSummaryQuery request, CancellationToken cancellationToken)
        {
            var eventStatistics = await context.EventStatistics.FirstOrDefaultAsync();
            if (eventStatistics != null)
            {
                return new EventSummaryQueryResponse { StatisticsExists = true, NumberOfSkaters = eventStatistics.NumberOfSkaters, CumulativeMiles = eventStatistics.CumulativeMiles };
            }

            var skaterMiles = (from entries in context.SkateLogEntries
                               join users in context.Users on entries.ApplicationUserId equals users.Id
                               where users.HasPaid
                               group entries by entries.ApplicationUserId into userEntries
                               select new
                               {
                                   UserId = userEntries.Key,
                                   TotalMiles = userEntries.Sum(x => x.DistanceInMiles),
                               }).ToList();

            return new EventSummaryQueryResponse
            {
                StatisticsExists = false,
                NumberOfSkaters = skaterMiles.Count,
                CumulativeMiles = skaterMiles.Sum(x => x.TotalMiles)
            };
        }
    }
}
