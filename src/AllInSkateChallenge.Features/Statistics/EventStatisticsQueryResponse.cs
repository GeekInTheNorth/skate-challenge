namespace AllInSkateChallenge.Features.Statistics
{
    using System.Collections.Generic;

    public class EventStatisticsQueryResponse
    {
        public SkaterStatisticsModel LongestTotalDistance { get; set; }

        public SkaterStatisticsModel LongestSingleDistance { get; set; }

        public SkaterStatisticsModel ShortestSingleDistance { get; set; }

        public List<StatisticsItemModel> SkateSessions { get; set; }

        public List<StatisticsItemModel> SkateDistances { get; set; }

        public decimal TotalMiles { get; set; }

        public int TotalSkateSessions { get; set; }
    }
}