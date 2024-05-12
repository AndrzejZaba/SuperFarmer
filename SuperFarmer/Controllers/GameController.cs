using Microsoft.AspNetCore.Mvc;
using SuperFarmer.Interfaces;

namespace SuperFarmer.Controllers
{
    public class GameController : Controller
    {
        private readonly IPlayerService _playerService;

        public GameController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public IActionResult PlayerPanel(int playerId)
        {
            

            return View(_playerService.GetNextPlayer());
        }
    }
}
