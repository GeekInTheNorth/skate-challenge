using System;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace AllInSkateChallenge.Features.Strava.Webhook.LogStravaIntegration
{
    public class LogStravaIntegrationCommandHandler : IRequestHandler<LogStravaIntegrationCommand>
    {
        private readonly ApplicationDbContext context;

        private readonly ILogger<LogStravaIntegrationCommandHandler> logger;

        public LogStravaIntegrationCommandHandler(ApplicationDbContext context, ILogger<LogStravaIntegrationCommandHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Unit> Handle(LogStravaIntegrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serializedEvent = JsonConvert.SerializeObject(request.Event, Formatting.Indented);
                var log = new StravaIntegrationLog
                {
                    Recieved = DateTime.Now,
                    Body = serializedEvent
                };

                context.StravaIntegrationLogs.Add(log);
                await context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "Failed to log strava webhook event", request.Event);
            }

            return Unit.Value;
        }
    }
}
