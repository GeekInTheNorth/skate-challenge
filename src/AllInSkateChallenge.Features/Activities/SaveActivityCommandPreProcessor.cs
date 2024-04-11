namespace AllInSkateChallenge.Features.Activities;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;

using MediatR.Pipeline;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            var userMiles = await context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(request.Skater.Id)).SumAsync(x => x.DistanceInMiles, cancellationToken);
            var targetCheckPoint = checkPointRepository.Get().First(x => x.SkateTarget.Equals(request.Skater.Target));

            if (userMiles > targetCheckPoint.DistanceInMiles)
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

    public int GetNewTarget(int oldTarget)
    {
        var targets = checkPointRepository.GetGoalCheckpoints();

        var nextTarget = targets.FirstOrDefault(x => x.SkateTarget > oldTarget) ??
                         targets.LastOrDefault();

        return nextTarget?.SkateTarget ?? oldTarget;
    }
}
