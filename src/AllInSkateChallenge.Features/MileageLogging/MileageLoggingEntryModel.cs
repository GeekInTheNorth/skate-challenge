using System.ComponentModel.DataAnnotations;

namespace AllInSkateChallenge.Features.MileageLogging
{
    public class MileageLoggingEntryModel
    {
        [Required]
        public DistanceUnit DistanceUnit { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Distance { get; set; }

        [DataType(DataType.Url)]
        public string ExerciseUrl { get; set; }

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
