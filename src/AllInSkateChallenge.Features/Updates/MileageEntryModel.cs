using System;

namespace AllInSkateChallenge.Features.Updates
{
    public class MileageEntryModel
    {
        public DateTime Logged { get; set; }

        public string Skater { get; set; }

        public string GravatarUrl { get; set; }

        public decimal Miles { get; set; }
    }
}
