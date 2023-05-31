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
                model.BestAverageSpeedMph = mileageEntries.Max(x => x.AverageSpeedInMph);
                model.BestTopSpeedMph = mileageEntries.Max(x => x.TopSpeedInMph);
                model.LowestElevationFeet = mileageEntries.Select(x => x.LowestElevationInFeet).OrderBy(x => x).FirstOrDefault(x => x != 0);
                model.HighestElevationFeet = mileageEntries.Select(x => x.HighestElevationInFeet).OrderByDescending(x => x).FirstOrDefault(x => x != 0);
                model.GreatestElevationGainFeet = mileageEntries.Max(x => x.ElevationGainInFeet);
                model.ShortestDistanceMiles = mileageEntries.Min(x => x.DistanceInMiles);
                model.LongestDistanceMiles = mileageEntries.Max(x => x.DistanceInMiles);
                model.TotalDistanceMiles = mileageEntries.Sum(x => x.DistanceInMiles);
                model.NumberOfSessions = mileageEntries.Count();
            }

            return View("~/Views/Shared/Components/PersonalStatistics/Default.cshtml", model);
        }
    }
}
