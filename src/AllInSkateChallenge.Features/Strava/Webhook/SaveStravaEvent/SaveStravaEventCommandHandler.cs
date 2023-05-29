using System;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Strava.Webhook.SaveStravaEvent
{
    public class SaveStravaEventCommandHandler : IRequestHandler<SaveStravaEventCommand>
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly ILogger<SaveStravaEventCommandHandler> logger;

        public SaveStravaEventCommandHandler(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<SaveStravaEventCommandHandler> logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task Handle(SaveStravaEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByNameAsync(request.StravaUserId);

                if (user == null)
                {
                    return;
                }

                var existingLogEntry = await context.SkateLogEntries.FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id && x.StravaId == request.ActivityId, cancellationToken);
                var existingEvent = await context.StravaEvents.FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id && x.StravaActivityId == request.ActivityId, cancellationToken);
                
                HandleEventNotExisting(request, user, existingLogEntry, existingEvent);
                HandleActivityTypeChange(request, existingLogEntry, existingEvent);
                HandlePreviouslyImported(existingLogEntry, existingEvent);
                HandleNameChange(request, existingLogEntry);

                await context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed when trying to add an event to a user", request);
            }
        }

        private void HandleEventNotExisting(SaveStravaEventCommand request, ApplicationUser user, SkateLogEntry existingLogEntry, StravaEvent existingStravaEvent)
        {
            if (existingStravaEvent == null)
            {
                var isImported = existingLogEntry != null;
                var newEntity = new StravaEvent { ApplicationUserId = user.Id, Imported = isImported, StravaActivityId = request.ActivityId };
                context.StravaEvents.Add(newEntity);
            }
        }

        private void HandleActivityTypeChange(SaveStravaEventCommand request, SkateLogEntry existingLogEntry, StravaEvent existingStravaEvent)
        {
            // If the activity has had a type change in Strava and previously the import did not result in a log entry
            // Then we want to mark it so that it can be imported again.
            if (existingLogEntry == null
                && existingStravaEvent != null
                && existingStravaEvent.Imported
                && request.Updates != null
                && request.Updates.ContainsKey("type"))
            {
                existingStravaEvent.Imported = false;
                context.StravaEvents.Update(existingStravaEvent);
            }
        }

        private void HandlePreviouslyImported(SkateLogEntry existingLogEntry, StravaEvent existingStravaEvent)
        {
            // If a skate log entry exists for this activity and the event is not marked as imported, then we need to correct the event.
            if (existingLogEntry != null && existingStravaEvent != null && !existingStravaEvent.Imported)
            {
                existingStravaEvent.Imported = true;
                context.StravaEvents.Update(existingStravaEvent);
            }
        }

        private void HandleNameChange(SaveStravaEventCommand request, SkateLogEntry existingLogEntry)
        {
            // If a log entry exists and the strava activity has had it's name changed, then update the log entry'
            if (existingLogEntry != null && request.Updates != null && request.Updates.ContainsKey("title"))
            {
                existingLogEntry.Name = request.Updates["title"];
                context.SkateLogEntries.Update(existingLogEntry);
            }
        }
    }
}
