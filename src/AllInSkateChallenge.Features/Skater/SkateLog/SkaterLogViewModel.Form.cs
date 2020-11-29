﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AllInSkateChallenge.Features.Skater.SkateLog
{

    public partial class SkaterLogViewModel : INewSkaterLogEntry
    {
        public string JourneyName { get; set; }

        [Required]
        public DistanceUnit DistanceUnit { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Distance { get; set; }

        public DateTime? DateSkated { get; set; }

        public decimal DistanceMiles
        {
            get
            {
                if (DistanceUnit.Equals(DistanceUnit.Miles))
                {
                    return Distance;
                }

                return Distance / 1.60934M;
            }
        }

        public decimal DistanceKilometres
        {
            get
            {
                if (DistanceUnit.Equals(DistanceUnit.Kilometres))
                {
                    return Distance;
                }

                return Distance * 1.60934M;
            }
        }
    }
}
