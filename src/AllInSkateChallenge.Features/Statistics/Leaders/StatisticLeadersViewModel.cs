using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Statistics.Leaders
{
    public class StatisticLeadersViewModel
    {
        public string StatisticTitle { get; set; }

        public List<SkaterStatisticsModel> Skaters { get; set; }
    }
}
