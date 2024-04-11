using System;
using AllInSkateChallenge.Features.Data.Kontent;

namespace AllInSkateChallenge.Features.Skater.Progress;

public class SkaterProgressCheckPoint : CheckPointModel
{
    public DateTime? DateAchieved { get; set; }
}
