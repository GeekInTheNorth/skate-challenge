using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Skater.PersonalStatistics
{
    public class PersonalStatisticsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<SkateLogEntry> mileageEntries)
        {
            var model = new PersonalStatisticsViewModel();

            if (mileageEntries != null && mileageEntries.Any())
            {
                model.BestAverageSpeed = mileageEntries.Max(x => x.AverageSpeedInMph);
                model.BestTopSpeed = mileageEntries.Max(x => x.TopSpeedInMph);
                model.LowestElevation = mileageEntries.Select(x => x.LowestElevationInFeet).OrderBy(x => x).FirstOrDefault(x => x != 0);
                model.HighestElevation = mileageEntries.Select(x => x.HighestElevationInFeet).OrderByDescending(x => x).FirstOrDefault(x => x != 0);
                model.GreatestElevationGain = mileageEntries.Max(x => x.ElevationGainInFeet);
                model.ShortestDistance = mileageEntries.Min(x => x.DistanceInMiles);
                model.LongestDistance = mileageEntries.Max(x => x.DistanceInMiles);
                model.TotalDistance = mileageEntries.Sum(x => x.DistanceInMiles);
                model.NumberOfSessions = mileageEntries.Count();
            }

            return View("~/Views/Shared/Components/PersonalStatistics/Default.cshtml", model);
        }
    }
}
