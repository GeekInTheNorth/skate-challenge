using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Activities
{
    public class IgnoreActivitiesCommandHandler : IRequestHandler<IgnoreActivitiesCommand, IgnoreActivitiesCommandResponse>
    {
        private readonly ApplicationDbContext context;

        public IgnoreActivitiesCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IgnoreActivitiesCommandResponse> Handle(IgnoreActivitiesCommand request, CancellationToken cancellationToken)
        {
            if (request.Skater == null || request.StravaActivityIds == null || !request.StravaActivityIds.Any())
            {
                return new IgnoreActivitiesCommandResponse { ActivitiesIgnored = 0 };
            }

            var stravaEvents = await context.StravaEvents.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).ToListAsync();
            foreach(var stravaActivityId in request.StravaActivityIds)
            {
                var existingStravaEvent = stravaEvents.FirstOrDefault(x => x.StravaActivityId.Equals(stravaActivityId, StringComparison.CurrentCultureIgnoreCase));
                if (existingStravaEvent == null)
                {
                    var newStravaEvent = new StravaEvent
                    {
                        ApplicationUserId = request.Skater.Id,
                        StravaActivityId = stravaActivityId,
                        Imported = true
                    };

                    context.StravaEvents.Add(newStravaEvent);
                }
                else if (existingStravaEvent != null && !existingStravaEvent.Imported)
                {
                    existingStravaEvent.Imported = true;
                    context.StravaEvents.Update(existingStravaEvent);
                }
            }

            return new IgnoreActivitiesCommandResponse { ActivitiesIgnored = await context.SaveChangesAsync() };
        }
    }
}
