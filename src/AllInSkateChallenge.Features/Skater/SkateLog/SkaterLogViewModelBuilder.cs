using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogViewModelBuilder : ISkaterLogViewModelBuilder
    {
        private readonly IMediator mediator;

        private ApplicationUser skater;

        private INewSkaterLogEntry newEntry;

        public SkaterLogViewModelBuilder(IMediator mediator)
        {
            this.mediator = mediator;
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

        public async Task<SkaterLogViewModel> Build()
        {
            var command = new SkaterLogQuery { Skater = skater };
            var commandResponse = await mediator.Send(command);
            var entries = commandResponse.Entries ?? new List<SkateLogEntry>();

            return new SkaterLogViewModel
            {
                TotalMiles = entries.Sum(x => x.DistanceInMiles),
                Entries = entries,
                IsStravaAccount = skater.IsStravaAccount,
                DistanceUnit = newEntry?.DistanceUnit ?? DistanceUnit.Miles,
                Distance = newEntry?.Distance ?? 0,
                ExerciseUrl = newEntry?.ExerciseUrl
            };
        }
    }
}
