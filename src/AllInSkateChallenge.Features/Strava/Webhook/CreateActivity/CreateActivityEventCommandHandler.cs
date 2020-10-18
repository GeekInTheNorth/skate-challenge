using System;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Strava.Webhook.CreateActivity
{
    public class CreateActivityEventCommandHandler : IRequestHandler<CreateActivityEventCommand>
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly ILogger<CreateActivityEventCommandHandler> logger;

        public CreateActivityEventCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<CreateActivityEventCommandHandler> logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<Unit> Handle(CreateActivityEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByNameAsync(request.StravaUserId);

                if (user == null) return Unit.Value;

                var isLogged = await context.SkateLogEntries.AnyAsync(x => x.ApplicationUserId.Equals(user.Id) && x.SkateLogEntryId.Equals(request.ActivityId));
                var existingEntity = await context.StravaEvents.FirstOrDefaultAsync(x => x.ApplicationUserId.Equals(user.Id) && x.StravaActivityId.Equals(request.ActivityId));

                if (existingEntity != null && isLogged && !existingEntity.Imported)
                {
                    existingEntity.Imported = true;
                    context.StravaEvents.Update(existingEntity);
                }

                if (existingEntity == null)
                {
                    var newEntity = new StravaEvent { ApplicationUserId = user.Id, Imported = isLogged, StravaActivityId = request.ActivityId };
                    context.StravaEvents.Add(newEntity);
                }

                await context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "Failed when trying to add an event to a user", request);
            }

            return Unit.Value;
        }
    }
}
