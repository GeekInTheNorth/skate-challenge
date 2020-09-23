using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class MileageEntry
    {
        public int MileageEntryId { get; set; }

        public Guid UserId { get; set; }

        public DateTime Logged { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal Miles { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal Kilometres { get; set; }

        [StringLength(500, ErrorMessage = "Exercise Url cannot exceed 500 characters.")]
        public string ExerciseUrl { get; set; }
    }
}
