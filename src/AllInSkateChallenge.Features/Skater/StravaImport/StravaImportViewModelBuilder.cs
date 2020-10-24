using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;
using AllInSkateChallenge.Features.Skater.SkateLog;
using AllInSkateChallenge.Features.Strava;

using MediatR;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportViewModelBuilder : PageViewModelBuilder<StravaImportViewModel>, IStravaImportViewModelBuilder
    {
        private readonly IStravaService stravaService;

        private readonly IMediator mediator;

        public StravaImportViewModelBuilder(IStravaService stravaService, IMediator mediator) : base(mediator)
        {
            this.stravaService = stravaService;
            this.mediator = mediator;
        }

        public override async Task<PageViewModel<StravaImportViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Strava Import";
            model.DisplayPageTitle = "Your Strava Activities";
            model.IntroductoryText = "Please note that the only Strava activity types which are eligible for the ALL IN Skate Challenge are Inline Skating, Ice Skating and Skateboarding.";
            model.DisplayStravaNotification = false;

            var stravaActivityListResponse = await stravaService.List(User);
            var command = new SkaterLogQuery { Skater = User };
            var commandResponse = await mediator.Send(command);
            var logEntries = commandResponse.Entries ?? new List<SkateLogEntry>();

            model.Content.Fault = stravaActivityListResponse.Faults;
            model.Content.Activities = GetActivityList(stravaActivityListResponse, logEntries);

            return model;
        }

        private List<StravaImportActivityViewModel> GetActivityList(StravaActivityListResponse stravaActivityListResponse, List<SkateLogEntry> logEntries)
        {
            if (stravaActivityListResponse?.Activities == null)
            {
                return new List<StravaImportActivityViewModel>();
            }

            var allowedTypes = new List<string> { "IceSkate", "InlineSkate", "Skateboard" };
            
            return stravaActivityListResponse.Activities.Select(x => new StravaImportActivityViewModel
            {
                ActivityId = x.ActivityId,
                ActivityType = x.ActivityType,
                Miles = x.DistanceMetres * 0.000621371M,
                StartDate = x.StartDate,
                EndDate = x.StartDate.AddSeconds(x.ElapsedTime),
                IsImported = logEntries.Any(y => y.StravaId != null && y.StravaId == x.ActivityId),
                IsEligableActivity = allowedTypes.Any(y => y.Equals(x.ActivityType, StringComparison.CurrentCultureIgnoreCase))
            }).ToList();
        }
    }
}
