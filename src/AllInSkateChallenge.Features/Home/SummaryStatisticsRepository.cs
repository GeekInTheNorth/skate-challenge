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
            var skaterMiles = (from entries in context.MileageEntries
                               group entries by entries.UserId into userEntries
                               select new
                               {
                                   UserId = userEntries.Key,
                                   TotalMiles = userEntries.Sum(x => x.Miles),
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
            var skaterMiles = context.MileageEntries.Where(x => x.UserId.Equals(userId)).Sum(x => x.Miles);

            return new SummaryStatisticsModel
            {
                NumberOfSkaters = 1,
                TotalMiles = skaterMiles
            };
        }
    }
}
