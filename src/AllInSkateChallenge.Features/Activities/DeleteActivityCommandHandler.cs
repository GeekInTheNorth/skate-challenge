using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Activities
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly ApplicationDbContext context;

        public DeleteActivityCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            // Clear matching logs
            var logsToDelete = await context.SkateLogEntries.Where(x => x.SkateLogEntryId.Equals(request.MileageEntryId) && x.ApplicationUserId.Equals(request.Skater.Id)).ToListAsync();
            context.SkateLogEntries.RemoveRange(logsToDelete);

            // Clear event statistics
            var statistics = await context.EventStatistics.ToListAsync();
            context.EventStatistics.RemoveRange(statistics);

            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
