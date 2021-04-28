using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Activities
{
    public class SaveActivityCommandHandler : IRequestHandler<SaveActivityCommand, SaveActivityCommandResult>
    {
        private readonly ApplicationDbContext context;

        private readonly ILogger<SaveActivityCommandHandler> logger;

        public SaveActivityCommandHandler(ApplicationDbContext context, ILogger<SaveActivityCommandHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<SaveActivityCommandResult> Handle(SaveActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var distance = ConvertToMiles(request.Distance, request.DistanceUnit);
                var elevation = ConvertToFeet(request.Elevation, request.ElevationUnit);
                
                // Create the new entry if it does not exist
                var activityLogged = request.StartDate ?? DateTime.Now;
                var recordExists = await RecordExists(request, activityLogged);
                var creatingRecord = false;
                if (!recordExists)
                {
                    var entry = new SkateLogEntry 
                    { 
                        ApplicationUserId = request.Skater.Id, 
                        StravaId = request.StavaActivityId, 
                        DistanceInMiles = distance, 
                        ElevationInFeet = elevation,
                        Logged = request.StartDate ?? DateTime.Now, 
                        Name = request.Name 
                    };
                    context.SkateLogEntries.Add(entry);
                    creatingRecord = true;
                }

                // mark any strava import event as imported
                var stravaEvent = context.StravaEvents.FirstOrDefault(x => x.StravaActivityId.Equals(request.StavaActivityId) && x.ApplicationUserId.Equals(request.Skater.Id) && !x.Imported);
                if (stravaEvent != null)
                {
                    stravaEvent.Imported = true;
                    context.StravaEvents.Update(stravaEvent);
                }

                // Clear event statistics
                var statistics = await context.EventStatistics.ToListAsync();
                context.EventStatistics.RemoveRange(statistics);

                await context.SaveChangesAsync();

                return new SaveActivityCommandResult { WasSuccessful = creatingRecord, RecordExists = recordExists };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to save mileage entry", request);
            }

            return new SaveActivityCommandResult() { WasSuccessful = false };
        }

        private async Task<bool> RecordExists(SaveActivityCommand request, DateTime activityLogged)
        {
            if (!string.IsNullOrWhiteSpace(request.StavaActivityId))
            {
                return await context.SkateLogEntries.AnyAsync(x => x.StravaId.Equals(request.StavaActivityId));
            }

            var upperThreshold = activityLogged.AddSeconds(30);
            var lowerThreshold = activityLogged.AddSeconds(-30);

            return await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id) && x.Name.Equals(request.Name) && x.Logged >= lowerThreshold && x.Logged <= upperThreshold).AnyAsync();
        }

        private decimal ConvertToMiles(decimal distance, DistanceUnit units)
        {
            switch (units)
            {
                case DistanceUnit.Kilometres:
                    return distance * 0.621371M;
                case DistanceUnit.Metres:
                    return distance * 0.000621371M;
                case DistanceUnit.Feet:
                    return distance / 5280M;
                default:
                    return distance;
            }
        }

        private decimal ConvertToFeet(decimal distance, DistanceUnit units)
        {
            switch (units)
            {
                case DistanceUnit.Kilometres:
                    return distance * 3280.84M;
                case DistanceUnit.Metres:
                    return distance * 3.28084M;
                case DistanceUnit.Miles:
                    return distance * 5280M;
                default:
                    return distance;
            }
        }
    }
}
