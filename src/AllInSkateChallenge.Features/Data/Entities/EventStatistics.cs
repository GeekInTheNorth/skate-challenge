using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllInSkateChallenge.Features.Data.Entities
{
    public class EventStatistic
    {
        public Guid EventStatisticId { get; set; }

        public int NumberOfSkaters { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal CumulativeMiles { get; set; }
    }
}
