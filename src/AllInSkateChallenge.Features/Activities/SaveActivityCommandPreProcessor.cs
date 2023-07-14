namespace AllInSkateChallenge.Features.Activities;

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

    public static SkateTarget GetNewTarget(SkateTarget oldTarget)
    {
        switch (oldTarget)
        {
            case SkateTarget.CornExchange:
            case SkateTarget.SoveriegnSquare:
            case SkateTarget.GranaryWharf:
            case SkateTarget.TetleyBreweryWharf:
            case SkateTarget.LeedsIndustrialMuseum:
            case SkateTarget.ArmleyPark:
            case SkateTarget.EllandRoad:
            case SkateTarget.MiddletonRailway:
            case SkateTarget.Carlton:
                return SkateTarget.TempleNewsamPark;
            case SkateTarget.TempleNewsamPark:
            case SkateTarget.LsTen:
            case SkateTarget.RoyalArmouriesMuseum:
            case SkateTarget.KirkgateMarket:
            case SkateTarget.LeedsGrandTheatre:
            case SkateTarget.MilleniumSquare:
            case SkateTarget.RamgarhiaSikhSportsCentre:
                return SkateTarget.PotternewtonPark;
            case SkateTarget.PotternewtonPark:
            case SkateTarget.MeanwoodValleyUrbanFarm:
            case SkateTarget.YorkshireCricketGround:
            case SkateTarget.KirkstallAbbey:
            case SkateTarget.SunnyBankMillsGallery:
            case SkateTarget.BrownleeCentre:
            case SkateTarget.GoldenAcrePark:
            case SkateTarget.EccupReservoir:
                return SkateTarget.EmmerdaleTheTour;
            case SkateTarget.EmmerdaleTheTour:
            case SkateTarget.HarewoodHouseTrust:
            case SkateTarget.OtleyChevinForestPark:
            case SkateTarget.YeadonTarn:
                return SkateTarget.LeedsBradfordAirport;
            case SkateTarget.LeedsBradfordAirport:
                return SkateTarget.ThereAndBackAgain;
            default:
                return oldTarget;
        }
    }
}
