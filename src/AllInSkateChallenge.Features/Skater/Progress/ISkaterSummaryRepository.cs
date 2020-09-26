using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Skater.Progress
{
    public interface ISkaterSummaryRepository
    {
        decimal GetTotalDistance(ApplicationUser applicationUser);
    }
}
