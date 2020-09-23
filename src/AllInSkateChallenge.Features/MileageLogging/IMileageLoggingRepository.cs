using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.MileageLogging
{
    public interface IMileageLoggingRepository
    {
        Task SaveAsync(IdentityUser user, MileageLoggingEntryModel mileageUpdate);
    }

    public class MileageLoggingRepository : IMileageLoggingRepository
    {
        private readonly ApplicationDbContext context;

        public MileageLoggingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync(IdentityUser user, MileageLoggingEntryModel mileageUpdate)
        {
            var newEntry = new MileageEntry
            {
                UserId = new Guid(user.Id),
                Logged = DateTime.Now,
                ExerciseUrl = mileageUpdate.ExerciseUrl,
                Miles = mileageUpdate.DistanceMiles,
                Kilometres = mileageUpdate.DistanceKilometres
            };

            await context.AddAsync(newEntry);
            await context.SaveChangesAsync();
        }
    }
}
