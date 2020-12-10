using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

using MediatR.Pipeline;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllInSkateChallenge.Features.Activities
{
    public class SaveActivityCommandPreProcessor : IRequestPreProcessor<SaveActivityCommand>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ApplicationDbContext context;

        private readonly ICheckPointRepository checkPointRepository;

        private readonly ILogger<SaveActivityCommandPreProcessor> logger;

        public SaveActivityCommandPreProcessor(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ICheckPointRepository checkPointRepository, 
            ILogger<SaveActivityCommandPreProcessor> logger)
        {
            this.userManager = userManager;
            this.context = context;
            this.checkPointRepository = checkPointRepository;
            this.logger = logger;
        }

        public async Task Process(SaveActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Skater.Target.Equals(SkateTarget.ThereAndBackAgain))
                {
                    // No need to update the user's target.
                    return;
                }

                var userMiles = await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).SumAsync(x => x.DistanceInMiles, cancellationToken);
                var targetCheckPoint = checkPointRepository.Get().First(x => x.SkateTarget.Equals(request.Skater.Target));

                if (userMiles > targetCheckPoint.Distance)
                {
                    request.Skater.Target = GetNewTarget(request.Skater.Target);
                    await userManager.UpdateAsync(request.Skater);
                }
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "Failed to update the user's personal target.");
            }
        }

        public SkateTarget GetNewTarget(SkateTarget oldTarget)
        {
            switch (oldTarget)
            {
                case SkateTarget.None:
                case SkateTarget.AireValleyMarina:
                    return SkateTarget.Saltaire;
                case SkateTarget.Saltaire:
                case SkateTarget.BingleyFiveRiseLocks:
                case SkateTarget.SkiptonCastle:
                case SkateTarget.EastMartonDoubleArchedBridge:
                    return SkateTarget.FoulridgeSummit;
                case SkateTarget.FoulridgeSummit:
                case SkateTarget.Burnley:
                case SkateTarget.HalfwayThere:
                case SkateTarget.BlackburnFlight:
                case SkateTarget.WiganPier:
                case SkateTarget.TheScotchPiperInn:
                    return SkateTarget.LiverpoolCanningDock;
                case SkateTarget.LiverpoolCanningDock:
                case SkateTarget.ThereAndBackAgain:
                    return SkateTarget.ThereAndBackAgain;
                default:
                    return oldTarget;
            }
        }
    }
}
