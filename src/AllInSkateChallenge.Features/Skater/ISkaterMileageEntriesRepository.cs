using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.MileageLogging;

namespace AllInSkateChallenge.Features.Skater
{
    public interface ISkaterMileageEntriesRepository
    {
        List<MileageEntry> GetEntries(ApplicationUser skater);

        decimal GetTotalDistance(ApplicationUser skater);

        Task SaveAsync(ApplicationUser skater, MileageLoggingEntryModel entry);

        Task DeleteAsync(int mileageEntryId);
    }
}
