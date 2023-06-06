namespace AllInSkateChallenge.Features.Statistics;

using System.Collections.Generic;

public class EventStatisticsViewModel
{
    public PeriodRange PeriodRange { get; set; }

    public SkaterStatisticsModel LongestTotalDistance { get; set; }

    public SkaterStatisticsModel LongestSingleDistance { get; set; }

    public SkaterStatisticsModel ShortestSingleDistance { get; set; }

    public SkaterStatisticsModel MostJourneys { get; set; }

    public SkaterStatisticsModel BestTopSpeed { get; set; }

    public SkaterStatisticsModel BestAverageSpeed { get; set; }

    public SkaterStatisticsModel GreatestClimb { get; set; }

    public SkaterStatisticsModel SkybornSkater { get; set; }

    public List<StatisticsItemModel> SkateSessions { get; set; }

    public List<StatisticsItemModel> SkateDistances { get; set; }

    public List<StatisticsDayItemModel> ActivitiesByDay { get; set; }

    public List<StatisticsDayItemModel> KilometresByDay { get; set; }

    public decimal TotalKilometres { get; set; }

    public int TotalSkateSessions { get; set; }

    public decimal KilometresByStrava { get; set; }

    public decimal KilometresByManual { get; set; }

    public int JourneysByStrava { get; set; }

    public int JourneysByManual { get; set; }

    public List<CheckPointStatisticsModel> CheckPoints { get; set; }
}