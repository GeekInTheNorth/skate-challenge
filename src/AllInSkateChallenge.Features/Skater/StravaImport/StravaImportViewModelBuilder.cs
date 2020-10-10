using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Strava;

namespace AllInSkateChallenge.Features.Skater.StravaImport
{
    public class StravaImportViewModelBuilder : IStravaImportViewModelBuilder
    {
        private readonly IStravaService stravaService;

        private readonly ISkaterMileageEntriesRepository entriesRepository;

        private ApplicationUser skater;

        public StravaImportViewModelBuilder(IStravaService stravaService, ISkaterMileageEntriesRepository entriesRepository)
        {
            this.stravaService = stravaService;
            this.entriesRepository = entriesRepository;
        }

        public IStravaImportViewModelBuilder WithUser(ApplicationUser skater)
        {
            this.skater = skater;

            return this;
        }

        public async Task<StravaImportViewModel> BuildAsync()
        {
            var stravaActivityListResponse = await stravaService.List(skater);
            var logEntries = await entriesRepository.GetSkateLogEntries(skater);
            
            return new StravaImportViewModel
            {
                Fault = stravaActivityListResponse.Faults,
                Activities = GetFilteredActivities(stravaActivityListResponse, logEntries)
            };
        }

        private List<StravaImportActivityViewModel> GetFilteredActivities(StravaActivityListResponse stravaActivityListResponse, List<SkateLogEntry> logEntries)
        {
            if (stravaActivityListResponse?.Activities == null)
            {
                return new List<StravaImportActivityViewModel>();
            }

            var allowedTypes = new List<string> { "IceSkate", "InlineSkate", "Skateboard" };
            var filteredEntries = stravaActivityListResponse.Activities.Where(x => allowedTypes.Any(y => y.Equals(x.ActivityType, StringComparison.CurrentCultureIgnoreCase))).OrderByDescending(x => x.StartDate).ToList();

            return filteredEntries.Select(x => new StravaImportActivityViewModel
            {
                ActivityId = x.ActivityId,
                ActivityType = x.ActivityType,
                Miles = x.DistanceMetres * 0.000621371M,
                StartDate = x.StartDate,
                EndDate = x.StartDate.AddSeconds(x.ElapsedTime),
                IsImported = logEntries.Any(y => y.StravaId != null && y.StravaId == x.ActivityId)
            }).ToList();
        }
    }
}
