using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Activities;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogViewModelBuilder : PageViewModelBuilder<SkaterLogViewModel>, ISkaterLogViewModelBuilder
    {
        private readonly IMediator mediator;

        private INewSkaterLogEntry newEntry;

        private SaveActivityCommandResult saveResponse;

        public SkaterLogViewModelBuilder(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        public ISkaterLogViewModelBuilder WithNewEntry(INewSkaterLogEntry newEntry)
        {
            this.newEntry = newEntry;

            return this;
        }

        public ISkaterLogViewModelBuilder WithSaveResponse(SaveActivityCommandResult saveResponse)
        {
            this.saveResponse = saveResponse;

            return this;
        }

        public override async Task<PageViewModel<SkaterLogViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Your Skate Log";
            model.DisplayPageTitle = "Your Skate Log";
            model.IsNoIndexPage = true;
            model.DisplayStravaNotification = false;
            model.IntroductoryText = "This screen allows you to create, view and delete your skate log entries.";
            
            var command = new SkaterLogQuery { Skater = User };
            var commandResponse = await mediator.Send(command);
            var entries = commandResponse.Entries ?? new List<SkateLogEntry>();

            model.Content.TotalMiles = entries.Sum(x => x.DistanceInMiles);
            model.Content.Entries = entries.OrderByDescending(x => x.Logged).ToList();
            model.Content.DistanceUnit = newEntry?.DistanceUnit ?? DistanceUnit.Miles;
            model.Content.Distance = newEntry?.Distance ?? 0;
            model.Content.VelocityUnit = newEntry?.VelocityUnit ?? VelocityUnit.MilesPerHour;
            model.Content.AverageSpeed = newEntry?.AverageSpeed ?? 0;
            model.Content.TopSpeed = newEntry?.TopSpeed ?? 0;
            model.Content.ElevationUnit = newEntry?.ElevationUnit ?? DistanceUnit.Feet;
            model.Content.LowestElevation = newEntry?.LowestElevation ?? 0;
            model.Content.HighestElevation = newEntry?.HighestElevation ?? 0;
            model.Content.ElevationGain = newEntry?.ElevationGain ?? 0;
            model.Content.DateSkated = newEntry?.DateSkated;
            model.Content.JourneyName = newEntry?.JourneyName;
            model.Content.RecordExists = saveResponse?.RecordExists ?? false;

            return model;
        }
    }
}
