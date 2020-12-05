using System.Linq;

namespace AllInSkateChallenge.Features.Data.Static
{
    public class CheckPointModel
    {
        public SkateTarget SkateTarget { get; set; }

        public decimal Distance { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string DigitalBadge { get; set; }

        public string DigitalBadgeName => DigitalBadge?.Split('/').Last();

        public string FinisherDigitalBadge { get; set; }

        public string FinisherDigitalBadgeName => FinisherDigitalBadge?.Split('/').Last();
    }
}
