using System;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;

using MediatR;

namespace AllInSkateChallenge.Features.Activities
{
    public class SaveActivityCommand : IRequest<SaveActivityCommandResult>
    {
        public ApplicationUser Skater { get; set; }

        public string StavaActivityId { get; set; }

        public DateTime? StartDate { get; set; }

        public string Name { get; set; }

        public decimal Distance { get; set; }

        public DistanceUnit DistanceUnit { get; set; }

        public decimal ElevationGain { get; set; }

        public DistanceUnit ElevationGainUnit { get; set; }

        public decimal LowestElevation { get; set; }
        
        public DistanceUnit LowestElevationUnit { get; set; }
        
        public decimal HighestElevation { get; set; }
        
        public DistanceUnit HighestElevationUnit { get; set; }
        
        public decimal TopSpeed { get; set; }
        
        public VelocityUnit TopSpeedUnit { get; set; }
        
        public decimal AverageSpeed { get; set; }
        
        public VelocityUnit AverageSpeedUnit { get; set; }
        
        public int Duration { get; set; }
    }
}
