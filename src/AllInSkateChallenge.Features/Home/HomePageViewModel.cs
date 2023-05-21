using AllInSkateChallenge.Features.Common;

namespace AllInSkateChallenge.Features.Home
{
    public class HomePageViewModel
    {
        public int NumberOfSkaters { get; set; }
        
        public decimal CumulativeMiles { get; set; }

        public decimal CumulativeKilometers => Conversion.MilesToKilometres(CumulativeMiles);
    }
}