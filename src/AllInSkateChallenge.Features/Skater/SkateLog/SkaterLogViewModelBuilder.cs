using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{
    public class SkaterLogViewModelBuilder : PageViewModelBuilder<SkaterLogViewModel>, ISkaterLogViewModelBuilder
    {
        private readonly IMediator mediator;

        private INewSkaterLogEntry newEntry;

        public SkaterLogViewModelBuilder(IMediator mediator, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
            this.mediator = mediator;
        }

        public IPageViewModelBuilder<SkaterLogViewModel> WithNewEntry(INewSkaterLogEntry newEntry)
        {
            this.newEntry = newEntry;

            return this;
        }

        public override async Task<PageViewModel<SkaterLogViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Your Skate Log";
            model.DisplayPageTitle = "Your Skate Log";
            model.DisplayStravaNotification = false;

            if (model.IsStravaUser)
            {
                model.IntroductoryText = "This screen allows you to create, view and delete your skate log entries. When Strava sends us notifications about your new or updated activities, you will be able to import those activities by clicking on 'Connect with STRAVA'.";
            }
            else
            {
                model.IntroductoryText = "This screen allows you to create, view and delete your skate log entries.";
            }

            var command = new SkaterLogQuery { Skater = User };
            var commandResponse = await mediator.Send(command);
            var entries = commandResponse.Entries ?? new List<SkateLogEntry>();

            model.Content.TotalMiles = entries.Sum(x => x.DistanceInMiles);
            model.Content.Entries = entries;
            model.Content.DistanceUnit = newEntry?.DistanceUnit ?? DistanceUnit.Miles;
            model.Content.Distance = newEntry?.Distance ?? 0;

            return model;
        }
    }
}
