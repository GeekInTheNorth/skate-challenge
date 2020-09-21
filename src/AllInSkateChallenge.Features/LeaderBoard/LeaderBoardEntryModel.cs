namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardEntryModel
    {
        public int Place { get; set; }

        public string Name { get; set; }

        public string GravatarUrl { get; set; }

        public decimal TotalMiles { get; set; }
    }
}
