using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Skater.SkateLog;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModelBuilder : PageViewModelBuilder<SkaterProgressViewModel>, ISkaterProgressViewModelBuilder
    {
        private readonly ICheckPointRepository checkPointRepository;

        private readonly IMediator mediator;

        public SkaterProgressViewModelBuilder(ICheckPointRepository checkPointRepository, IMediator mediator) : base(mediator)
        {
            this.checkPointRepository = checkPointRepository;
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<SkaterProgressViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Your Progress";
            model.DisplayPageTitle = "Your Progress";
            model.IsNoIndexPage = true;

            var command = new SkaterLogQuery { Skater = User };
            var commandResponse = await mediator.Send(command);
            var mileageEntries = commandResponse.Entries ?? new List<SkateLogEntry>();
            var totalDistance = mileageEntries.Sum(x => x.DistanceInMiles);
            var checkPoints = checkPointRepository.Get();
            var checkPointsReached = checkPoints.Where(x => x.Distance <= totalDistance).OrderBy(x => x.Distance);
            var targetCheckPoint = checkPoints.First(x => x.SkateTarget.Equals(User.Target));
            var nextCheckPoint = checkPoints.Where(x => x.Distance > totalDistance && x.Distance <= targetCheckPoint.Distance).OrderBy(x => x.Distance).FirstOrDefault();

            model.Content.CheckPointsReached = checkPointsReached.ToList();
            model.Content.MilesSkated = totalDistance;
            model.Content.TargetMiles = targetCheckPoint.Distance;
            model.Content.Entries = mileageEntries;

            if (nextCheckPoint != null)
            {
                var distanceToNextCheckpoint = nextCheckPoint.Distance - totalDistance;
                model.Content.NextCheckPoint = new CheckPointModel
                {
                    Title = "Your Next Checkpoint",
                    Description = $"You have to skate a further {distanceToNextCheckpoint:F2} miles to reach {nextCheckPoint.Title}.",
                    Longitude = nextCheckPoint.Longitude,
                    Latitude = nextCheckPoint.Latitude,
                    Url = nextCheckPoint.Url
                };
            }
            else
            {
                model.Content.NextCheckPoint = model.Content.CheckPointsReached.Last();
                model.Content.CheckPointsReached.Remove(model.Content.NextCheckPoint);
            }

            return model;
        }
    }
}
