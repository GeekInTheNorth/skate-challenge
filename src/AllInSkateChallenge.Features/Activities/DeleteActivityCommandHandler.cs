using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Command;
using AllInSkateChallenge.Features.Skater;

namespace AllInSkateChallenge.Features.Activities
{
    public class DeleteActivityCommandHandler : ICommandHandler<DeleteActivityCommand>
    {
        private readonly ISkaterMileageEntriesRepository repository;

        public DeleteActivityCommandHandler(ISkaterMileageEntriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> HandleAsync(DeleteActivityCommand command)
        {
            try
            {
                await repository.DeleteAsync(command.Skater, command.MileageEntryId);

                return new CommandResult() { IsSuccess = true };
            }
            catch (Exception)
            {
                return new CommandResult() { IsSuccess = false };
            }
        }
    }
}
