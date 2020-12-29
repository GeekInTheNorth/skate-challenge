namespace AllInSkateChallenge.Features.Home
{
    public class EventSummaryQueryResponse
    {
        public bool StatisticsExists { get; set; }

        public int NumberOfSkaters { get; set; }

        public decimal CumulativeMiles { get; set; }
    }
}
