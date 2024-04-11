namespace AllInSkateChallenge.Features.Skater.Progress;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Kontent;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Skater.SkateLog;
using AllInSkateChallenge.Features.Statistics;

using MediatR;

public class SkaterProgressViewModelBuilder : PageViewModelBuilder<SkaterProgressViewModel>, ISkaterProgressViewModelBuilder
{
    private readonly ICheckPointRepository checkPointRepository;

    private readonly IMediator mediator;

    private readonly ISkaterTargetAnalyser skaterTargetAnalyser;

    public SkaterProgressViewModelBuilder(
        ICheckPointRepository checkPointRepository, 
        IMediator mediator,
        ISkaterTargetAnalyser skaterTargetAnalyser) : base(mediator)
    {
        this.checkPointRepository = checkPointRepository;
        this.mediator = mediator;
        this.skaterTargetAnalyser = skaterTargetAnalyser;
    }

    public override async Task<PageViewModel<SkaterProgressViewModel>> Build()
    {
        var command = new SkaterLogQuery { Skater = User };
        var commandResponse = await mediator.Send(command);
        var skateLogEntries = commandResponse.Entries ?? new List<SkateLogEntry>();
        var analysis = skaterTargetAnalyser.Analyse(User, skateLogEntries);

        var totalDistance = skateLogEntries.Sum(x => x.DistanceInKilometres);
        var checkPoints = checkPointRepository.Get();
        var checkPointsReached = checkPoints.Where(x => x.DistanceInKilometers <= totalDistance).OrderBy(x => x.DistanceInKilometers);
        var targetCheckPoint = checkPoints.First(x => x.SkateTarget.Equals(User.Target));
        var nextCheckPoint = checkPoints.Where(x => x.DistanceInKilometers > totalDistance && x.DistanceInKilometers <= targetCheckPoint.DistanceInKilometers).OrderBy(x => x.DistanceInKilometers).FirstOrDefault();

        var model = await base.Build();
        model.PageTitle = "Your Progress";
        model.DisplayPageTitle = "Your Progress";
        model.IsNoIndexPage = true;
        model.Content.KilometersSkated = totalDistance;
        model.Content.TargetKilometers = targetCheckPoint.DistanceInKilometers;
        model.Content.Entries = skateLogEntries;

        model.Content.CheckPointsReached =
            (from cp in checkPointsReached
             join acp in analysis.CheckPointDates on cp.SkateTarget equals acp.Key
             orderby acp.Value
             select new SkaterProgressCheckPoint
             {
                 SkateTarget = cp.SkateTarget,
                 DistanceInKilometers = cp.DistanceInKilometers,
                 Title = cp.Title,
                 Description = cp.Description,
                 Longitude = cp.Longitude,
                 Latitude = cp.Latitude,
                 Links = cp.Links,
                 Image = cp.Image,
                 DigitalBadge = cp.DigitalBadge,
                 FinisherDigitalBadge = cp.FinisherDigitalBadge,
                 DateAchieved = acp.Value
             }).ToList();

        if (nextCheckPoint != null)
        {
            var distanceToNextCheckpoint = nextCheckPoint.DistanceInKilometers - totalDistance;
            model.Content.NextCheckPoint = new SkaterProgressCheckPoint
            {
                Title = "Your Next Checkpoint",
                Description = $"You have to skate a further {distanceToNextCheckpoint:F2} kilometres to reach {nextCheckPoint.Title}.",
                Longitude = nextCheckPoint.Longitude,
                Latitude = nextCheckPoint.Latitude,
                Links = nextCheckPoint.Links
            };
        }
        else
        {
            var lastCheckPointReached = model.Content.CheckPointsReached.Last();
            model.Content.NextCheckPoint = lastCheckPointReached;
            model.Content.CheckPointsReached.Remove(lastCheckPointReached);
        }

        return model;
    }
}