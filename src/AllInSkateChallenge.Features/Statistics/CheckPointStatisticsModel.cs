namespace AllInSkateChallenge.Features.Statistics
{
    using AllInSkateChallenge.Features.Data.Static;

    public class CheckPointStatisticsModel
    {
        public SkateTarget Target { get; set; }

        public string CheckPointName { get; set; }

        public string FirstSkaterName { get; set; }

        public string FirstSkaterProfile { get; set; }

        public string LatestSkaterName { get; set; }

        public string LatestSkaterProfile { get; set; }
    }
}