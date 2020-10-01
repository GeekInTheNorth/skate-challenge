using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    [Authorize]
    public class LeaderBoardController : Controller
    {
        private readonly ILeaderBoardQuery leaderBoardQuery;

        public LeaderBoardController(ILeaderBoardQuery leaderBoardQuery)
        {
            this.leaderBoardQuery = leaderBoardQuery;
        }

        public IActionResult Index()
        {
            var model = leaderBoardQuery.Get(10000);

            return View(model);
        }
    }
}
