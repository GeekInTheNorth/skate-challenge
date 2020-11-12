namespace AllInSkateChallenge.Features.Home
{
    public class EventStatisticsQueryResponse
    {
        public bool StatisticsExists { get; set; }

        public int NumberOfSkaters { get; set; }

        public decimal CumulativeMiles { get; set; }
    }
}
