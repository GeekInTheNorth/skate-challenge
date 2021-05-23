namespace AllInSkateChallenge.Features.Skater.PersonalStatistics
{
    public class PersonalStatisticsViewModel
    {
        public decimal LongestDistance { get; set; }

        public decimal ShortestDistance { get; set; }

        public decimal TotalDistance { get; set; }

        public bool ShowDistance => LongestDistance != 0 || ShortestDistance != 0 || TotalDistance != 0;

        public decimal BestAverageSpeed { get; set; }

        public decimal BestTopSpeed { get; set; }

        public decimal NumberOfSessions { get; set; }

        public bool ShowSpeed => BestAverageSpeed != 0 || BestTopSpeed != 0;

        public decimal HighestElevation { get; set; }

        public decimal LowestElevation { get; set; }

        public decimal GreatestElevationGain { get; set; }

        public bool ShowElevations => HighestElevation != 0 || LowestElevation != 0 || GreatestElevationGain != 0;

        public bool ShowPersonalStatistics => ShowDistance || ShowSpeed || ShowElevations;
    }
}
