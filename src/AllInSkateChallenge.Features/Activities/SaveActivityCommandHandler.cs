using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Common;
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
                        DistanceInMiles = ConvertToMiles(request.Distance, request.DistanceUnit),
                        ElevationGainInFeet = ConvertToFeet(request.ElevationGain, request.ElevationGainUnit),
                        LowestElevationInFeet = ConvertToFeet(request.LowestElevation, request.LowestElevationUnit),
                        HighestElevationInFeet = ConvertToFeet(request.HighestElevation, request.HighestElevationUnit),
                        AverageSpeedInMph = ConvertToMph(request.AverageSpeed, request.AverageSpeedUnit),
                        TopSpeedInMph = ConvertToMph(request.TopSpeed, request.TopSpeedUnit),
                        Logged = request.StartDate ?? DateTime.Now,
                        Duration = request.Duration,
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
                    return Conversion.KilometresToMiles(distance);
                case DistanceUnit.Metres:
                    return Conversion.MetresToMiles(distance);
                case DistanceUnit.Feet:
                    return Conversion.FeetToMiles(distance);
                default:
                    return distance;
            }
        }

        private decimal ConvertToFeet(decimal distance, DistanceUnit units)
        {
            switch (units)
            {
                case DistanceUnit.Kilometres:
                    return Conversion.KilometresToFeet(distance);
                case DistanceUnit.Metres:
                    return Conversion.MetresToFeet(distance);
                case DistanceUnit.Miles:
                    return Conversion.MilesToFeet(distance);
                default:
                    return distance;
            }
        }

        private decimal ConvertToMph(decimal velocity, VelocityUnit units)
        {
            switch (units)
            {
                case VelocityUnit.MetresPerSecond:
                    return Conversion.MetresPerSecondToMilesPerHour(velocity);
                case VelocityUnit.KilometersPerHour:
                    return Conversion.KilometresPerHourToMilesPerHour(velocity);
                default:
                    return velocity;
            }
        }
    }
}
