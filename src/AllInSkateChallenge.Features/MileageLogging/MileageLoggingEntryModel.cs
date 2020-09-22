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
    }
}
