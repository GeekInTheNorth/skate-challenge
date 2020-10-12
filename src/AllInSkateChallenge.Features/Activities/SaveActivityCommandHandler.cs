using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Command;
using AllInSkateChallenge.Features.Skater;

namespace AllInSkateChallenge.Features.Activities
{
    public class SaveActivityCommandHandler : ICommandHandler<SaveActivityCommand>
    {
        private readonly ISkaterMileageEntriesRepository repository;

        public SaveActivityCommandHandler(ISkaterMileageEntriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> HandleAsync(SaveActivityCommand command)
        {
            var distance = command.Distance;
            switch (command.DistanceUnit)
            {
                case DistanceUnit.Kilometres:
                    distance = distance * 0.621371M;
                    break;
                case DistanceUnit.Metres:
                    distance = distance * 0.000621371M;
                    break;
            }

            try
            {
                await repository.Save(command.Skater, command.StartDate ?? DateTime.Now, command.StavaActivityId, distance);

                return new CommandResult() { IsSuccess = true };
            }
            catch(Exception)
            {
                // TODO: Log this
                return new CommandResult() { IsSuccess = false };
            }
        }
    }
}
