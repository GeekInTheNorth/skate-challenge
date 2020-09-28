using System.Linq;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModelBuilder : ISkaterProgressViewModelBuilder
    {
        private readonly ICheckPointRepository checkPointRepository;

        private readonly ISkaterMileageEntriesRepository skaterSummaryRepository;

        private ApplicationUser skater;

        public SkaterProgressViewModelBuilder(ICheckPointRepository checkPointRepository, ISkaterMileageEntriesRepository skaterSummaryRepository)
        {
            this.checkPointRepository = checkPointRepository;
            this.skaterSummaryRepository = skaterSummaryRepository;
        }

        public ISkaterProgressViewModelBuilder WithUser(ApplicationUser skater)
        {
            this.skater = skater;

            return this;
        }

        public SkaterProgressViewModel Build()
        {
            var totalDistance = skaterSummaryRepository.GetTotalDistance(skater);
            var checkPoints = checkPointRepository.Get();
            var checkPointsReached = checkPoints.Where(x => x.Distance <= totalDistance).OrderBy(x => x.Distance);
            var nextCheckPoint = checkPoints.Where(x => x.Distance > totalDistance).OrderBy(x => x.Distance).FirstOrDefault();

            var model = new SkaterProgressViewModel
            {
                Title = "Your Progress",
                CheckPointsReached = checkPointsReached.ToList(),
                MilesSkated = totalDistance
            };

            if (nextCheckPoint != null)
            {
                var distanceToNextCheckpoint = nextCheckPoint.Distance - totalDistance;
                model.NextCheckPoint = new CheckPointModel
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
                model.NextCheckPoint = model.CheckPointsReached.Last();
                model.CheckPointsReached.Remove(model.NextCheckPoint);
            }

            return model;
        }
    }
}
