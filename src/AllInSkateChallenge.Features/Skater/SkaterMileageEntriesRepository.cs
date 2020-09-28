using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.MileageLogging;

namespace AllInSkateChallenge.Features.Skater
{
    public class SkaterMileageEntriesRepository : ISkaterMileageEntriesRepository
    {
        private readonly ApplicationDbContext context;

        public SkaterMileageEntriesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<MileageEntry> GetEntries(ApplicationUser skater)
        {
            var userId = new Guid(skater.Id);

            return context.MileageEntries.Where(x => x.UserId.Equals(userId)).OrderBy(x => x.Logged).ToList();
        }

        public decimal GetTotalDistance(ApplicationUser skater)
        {
            var userId = new Guid(skater.Id);

            return context.MileageEntries.Where(x => x.UserId.Equals(userId)).Sum(x => x.Miles);
        }

        public async Task SaveAsync(ApplicationUser skater, MileageLoggingEntryModel entry)
        {
            var newEntry = new MileageEntry
            {
                UserId = new Guid(skater.Id),
                Logged = DateTime.Now,
                ExerciseUrl = entry.ExerciseUrl,
                Miles = entry.DistanceMiles,
                Kilometres = entry.DistanceKilometres
            };

            await context.AddAsync(newEntry);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser skater, int mileageEntryId)
        {
            var userId = new Guid(skater.Id);
            var itemToDelete = context.MileageEntries.FirstOrDefault(x => x.MileageEntryId.Equals(mileageEntryId) && x.UserId.Equals(userId));

            if (itemToDelete != null)
            {
                context.MileageEntries.Remove(itemToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
