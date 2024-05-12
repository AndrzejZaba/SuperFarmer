using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;
using System.Diagnostics;

namespace SuperFarmer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGamePreparationService _gamePreparationService;

        public HomeController(
            ILogger<HomeController> logger,
            IGamePreparationService gamePreparationService
            )
        {
            _logger = logger;
            _gamePreparationService = gamePreparationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/StartGame/{players}")]
        public IActionResult StartGame(int players)
        {
            _gamePreparationService.PrepareGame(players);

            return RedirectToAction("Privacy");
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