namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesViewComponentModel
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public string LatestUpdatesUrl { get; set; }

        public bool ShowLoadMore { get; set; }
    }
}
