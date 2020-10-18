using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogQueryHandler : IRequestHandler<SkaterLogQuery, SkaterLogQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public SkaterLogQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<SkaterLogQueryResponse> Handle(SkaterLogQuery request, CancellationToken cancellationToken)
        {
            return new SkaterLogQueryResponse
            {
                Entries = await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).ToListAsync()
            };
        }
    }
}
