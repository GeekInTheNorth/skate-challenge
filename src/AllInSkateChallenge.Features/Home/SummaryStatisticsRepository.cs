using System;
using System.Linq;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Home
{
    public class SummaryStatisticsRepository : ISummaryStatisticsRepository
    {
        private readonly ApplicationDbContext context;

        public SummaryStatisticsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public SummaryStatisticsModel Get()
        {
            var skaterMiles = (from entries in context.SkateLogEntries
                               group entries by entries.ApplicationUserId into userEntries
                               select new
                               {
                                   UserId = userEntries.Key,
                                   TotalMiles = userEntries.Sum(x => x.DistanceInMiles),
                               }).ToList();

            return new SummaryStatisticsModel
            {
                NumberOfSkaters = skaterMiles.Count,
                TotalMiles = skaterMiles.Sum(x => x.TotalMiles)
            };
        }

        public SummaryStatisticsModel Get(ApplicationUser skater)
        {
            var userId = new Guid(skater.Id);
            var skaterMiles = context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(skater.Id)).Sum(x => x.DistanceInMiles);

            return new SummaryStatisticsModel
            {
                NumberOfSkaters = 1,
                TotalMiles = skaterMiles
            };
        }
    }
}
