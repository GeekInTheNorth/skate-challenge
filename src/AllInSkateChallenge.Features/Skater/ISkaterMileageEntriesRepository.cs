using System.Collections.Generic;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater
{
    public interface ISkaterMileageEntriesRepository
    {
        Task<List<SkateLogEntry>> GetSkateLogEntries(ApplicationUser skater);

        decimal GetTotalDistance(ApplicationUser skater);
    }
}
