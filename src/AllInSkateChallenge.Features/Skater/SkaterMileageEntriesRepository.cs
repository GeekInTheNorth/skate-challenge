using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Skater
{
    public class SkaterMileageEntriesRepository : ISkaterMileageEntriesRepository
    {
        private readonly ApplicationDbContext context;

        public SkaterMileageEntriesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SkateLogEntry>> GetSkateLogEntries(ApplicationUser skater)
        {
            return await context.SkateLogEntries.Where(x => x.ApplicationUserId == skater.Id).OrderByDescending(x => x.Logged).ToListAsync();
        }

        public async Task Save(ApplicationUser skater, DateTime logged, string stravaId, decimal miles)
        {
            var recordExists = !string.IsNullOrWhiteSpace(stravaId) && context.SkateLogEntries.Any(x => x.StravaId.Equals(stravaId));

            if (!recordExists)
            {
                var entry = new SkateLogEntry { ApplicationUserId = skater.Id, StravaId = stravaId, DistanceInMiles = miles, Logged = logged };
                context.SkateLogEntries.Add(entry);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(ApplicationUser skater, Guid mileageEntryId)
        {
            var itemToDelete = context.SkateLogEntries.FirstOrDefault(x => x.SkateLogEntryId.Equals(mileageEntryId) && x.ApplicationUserId.Equals(skater.Id));

            if (itemToDelete != null)
            {
                context.SkateLogEntries.Remove(itemToDelete);
                await context.SaveChangesAsync();
            }
        }

        public decimal GetTotalDistance(ApplicationUser skater)
        {
            var userId = new Guid(skater.Id);

            return context.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(skater.Id)).Sum(x => x.DistanceInMiles);
        }
    }
}
