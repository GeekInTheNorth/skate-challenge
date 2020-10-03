using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Home
{
    public interface ISummaryStatisticsRepository
    {
        SummaryStatisticsModel Get();

        SummaryStatisticsModel Get(ApplicationUser skater);
    }
}
