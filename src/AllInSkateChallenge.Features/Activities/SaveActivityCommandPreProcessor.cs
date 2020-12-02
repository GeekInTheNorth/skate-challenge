using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Extensions;

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

        private readonly ILogger<SaveActivityCommandHandler> logger;

        public SaveActivityCommandPreProcessor(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ICheckPointRepository checkPointRepository, 
            ILogger<SaveActivityCommandHandler> logger)
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

                var userMiles = await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).SumAsync(x => x.DistanceInMiles);
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

        private SkateTarget GetNewTarget(SkateTarget oldTarget)
        {
            if (oldTarget <= SkateTarget.Saltaire) return SkateTarget.FoulridgeSummit;

            return SkateTarget.LiverpoolCanningDock;
        }
    }
}
