using System;
using System.Linq;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public class SkaterSummaryRepository : ISkaterSummaryRepository
    {
        private readonly ApplicationDbContext context;

        public SkaterSummaryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public decimal GetTotalDistance(ApplicationUser applicationUser)
        {
            var userId = new Guid(applicationUser.Id);

            return context.MileageEntries.Where(x => x.UserId.Equals(userId)).Sum(x => x.Miles);
        }
    }
}
