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
            var model = new PersonalStatisticsViewModel
            {
                BestAverageSpeed = mileageEntries.Max(x => x.AverageSpeedInMph),
                BestTopSpeed = mileageEntries.Max(x => x.TopSpeedInMph),
                LowestElevation = mileageEntries.Where(x => x.LowestElevationInFeet != 0).Min(x => x.LowestElevationInFeet),
                HighestElevation = mileageEntries.Where(x => x.HighestElevationInFeet != 0).Max(x => x.HighestElevationInFeet),
                GreatestElevationGain = mileageEntries.Max(x => x.ElevationGainInFeet),
                ShortestDistance = mileageEntries.Min(x => x.DistanceInMiles),
                LongestDistance = mileageEntries.Max(x => x.DistanceInMiles),
                TotalDistance = mileageEntries.Sum(x => x.DistanceInMiles),
                NumberOfSessions = mileageEntries.Count()
            };

            return View("~/Views/Shared/Components/PersonalStatistics/Default.cshtml", model);
        }
    }
}
