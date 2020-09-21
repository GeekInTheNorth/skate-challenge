using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AllInSkateChallenge.Models;
using AllInSkateChallenge.Features.Home;

namespace AllInSkateChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHomePageViewModelBuilder viewModelBuilder;

        public HomeController(ILogger<HomeController> logger, IHomePageViewModelBuilder viewModelBuilder)
        {
            _logger = logger;
            this.viewModelBuilder = viewModelBuilder;
        }

        public IActionResult Index()
        {
            var model = viewModelBuilder.Build();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
