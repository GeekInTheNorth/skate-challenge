using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

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
            var itemToDelete = context.SkateLogEntries.FirstOrDefault(x => x.SkateLogEntryId.Equals(request.MileageEntryId) && x.ApplicationUserId.Equals(request.Skater.Id));

            if (itemToDelete == null)
            {
                throw new EntityNotFoundException(typeof(SkateLogEntry), request.MileageEntryId);
            }

            context.SkateLogEntries.Remove(itemToDelete);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
