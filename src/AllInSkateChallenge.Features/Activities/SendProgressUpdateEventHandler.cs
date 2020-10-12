using System;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Command;
using AllInSkateChallenge.Features.Skater;

namespace AllInSkateChallenge.Features.Activities
{
    public class SendProgressUpdateEventHandler : ICommandEventHandler<SaveActivityCommand>
    {
        private readonly ISkaterMileageEntriesRepository repository;

        private readonly ICheckPointRepository checkPointRepository;

        public SendProgressUpdateEventHandler(ISkaterMileageEntriesRepository repository, ICheckPointRepository checkPointRepository)
        {
            this.repository = repository;
            this.checkPointRepository = checkPointRepository;
        }

        public async Task HandleAsync(SaveActivityCommand command, CommandResult result)
        {
            if (!result.IsSuccess) return;

            try
            {
                var milesThisSkate = command.Distance;
                switch (command.DistanceUnit)
                {
                    case DistanceUnit.Kilometres:
                        milesThisSkate = milesThisSkate * 0.621371M;
                        break;
                    case DistanceUnit.Metres:
                        milesThisSkate = milesThisSkate * 0.000621371M;
                        break;
                }

                var totalMiles = repository.GetTotalDistance(command.Skater);
                var previousMiles = totalMiles - milesThisSkate;
                var checkPointReached = checkPointRepository.Get().Where(x => x.Distance >= previousMiles && x.Distance <= totalMiles).OrderByDescending(x => x.Distance).FirstOrDefault();

                if (checkPointReached != null)
                {
                    // TODO: Send Email
                }
            }
            catch (Exception)
            {
                // TODO: Log this
            }
        }
    }
}
