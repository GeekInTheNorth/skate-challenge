using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater
{
    public interface ISkaterMileageEntriesRepository
    {
        Task<List<SkateLogEntry>> GetSkateLogEntries(ApplicationUser skater);

        Task Save(ApplicationUser skater, DateTime logged, string stravaId, decimal miles);

        Task DeleteAsync(ApplicationUser skater, Guid mileageEntryId);

        decimal GetTotalDistance(ApplicationUser skater);
    }
}
