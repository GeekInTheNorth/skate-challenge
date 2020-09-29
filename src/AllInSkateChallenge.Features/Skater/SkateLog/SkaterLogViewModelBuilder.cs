using System.Linq;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogViewModelBuilder : ISkaterLogViewModelBuilder
    {
        private readonly ISkaterMileageEntriesRepository repository;

        private ApplicationUser skater;

        private INewSkaterLogEntry newEntry;

        public SkaterLogViewModelBuilder(ISkaterMileageEntriesRepository repository)
        {
            this.repository = repository;
        }

        public ISkaterLogViewModelBuilder WithNewEntry(INewSkaterLogEntry newEntry)
        {
            this.newEntry = newEntry;

            return this;
        }

        public ISkaterLogViewModelBuilder WithUser(ApplicationUser skater)
        {
            this.skater = skater;

            return this;
        }

        public SkaterLogViewModel Build()
        {
            var entries = repository.GetEntries(skater);

            return new SkaterLogViewModel
            {
                TotalMiles = entries.Sum(x => x.Miles),
                Entries = entries,
                DistanceUnit = newEntry?.DistanceUnit ?? DistanceUnit.Miles,
                Distance = newEntry?.Distance ?? 0,
                ExerciseUrl = newEntry?.ExerciseUrl
            };
        }
    }
}
