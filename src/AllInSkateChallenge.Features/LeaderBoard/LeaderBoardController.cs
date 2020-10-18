using System.Threading.Tasks;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    [Authorize]
    public class LeaderBoardController : Controller
    {
        private readonly IMediator mediator;

        public LeaderBoardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var model = await mediator.Send(new LeaderBoardQuery());

            return View(model);
        }
    }
}
