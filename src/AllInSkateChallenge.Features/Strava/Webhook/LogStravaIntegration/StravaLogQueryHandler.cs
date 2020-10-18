using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration
{
    public class StravaLogQueryHandler : IRequestHandler<StravaLogQuery, StravaLogQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public StravaLogQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StravaLogQueryResponse> Handle(StravaLogQuery request, CancellationToken cancellationToken)
        {
            var filterDate = DateTime.Now.AddDays(-1 * request.NumberOfDays);

            return new StravaLogQueryResponse
            {
                Logs = await context.StravaIntegrationLogs.Where(x => x.Recieved >= filterDate).OrderBy(x => x.Recieved).ToListAsync()
            };
        }
    }
}
