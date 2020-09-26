using System.Linq;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterProgressViewModelBuilder : ISkaterProgressViewModelBuilder
    {
        private readonly ICheckPointRepository checkPointRepository;

        private readonly ISkaterSummaryRepository skaterSummaryRepository;

        private ApplicationUser applicationUser;

        public SkaterProgressViewModelBuilder(ICheckPointRepository checkPointRepository, ISkaterSummaryRepository skaterSummaryRepository)
        {
            this.checkPointRepository = checkPointRepository;
            this.skaterSummaryRepository = skaterSummaryRepository;
        }

        public ISkaterProgressViewModelBuilder WithUser(ApplicationUser applicationUser)
        {
            this.applicationUser = applicationUser;

            return this;
        }

        public SkaterProgressViewModel Build()
        {
            var totalDistance = skaterSummaryRepository.GetTotalDistance(applicationUser);
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
                model.NextCheckPointDescription = $"You have to skate a further {distanceToNextCheckpoint:F2} miles to reach {nextCheckPoint.Title}.";
            }

            return model;
        }
    }
}
