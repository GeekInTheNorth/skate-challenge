using System;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater;

namespace AllInSkateChallenge.Features.Activities
{
    public class SaveActivityCommand
    {
        public ApplicationUser Skater { get; set; }

        public string StavaActivityId { get; set; }

        public decimal Distance { get; set; }

        public DistanceUnit DistanceUnit { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
