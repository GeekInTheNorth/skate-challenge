using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.Registration
{
    public class RegistrationAvailabilityQueryHandler : IRequestHandler<RegistrationAvailabilityQuery, bool>
    {
        public Task<bool> Handle(RegistrationAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var isEventRegistrationOpen = DateTime.Today >= new DateTime(2023, 5, 1);

            return Task.FromResult(isEventRegistrationOpen);
        }
    }
}
