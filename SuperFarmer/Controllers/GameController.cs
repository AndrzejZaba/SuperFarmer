using Microsoft.AspNetCore.Mvc;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Controllers
{
    public class GameController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IDiceService _diceService;

        public GameController(IPlayerService playerService, IDiceService diceService)
        {
            _playerService = playerService;
            _diceService = diceService;
        }

        [HttpGet]
        [Route("Game/PlayerPanel/{diceRoll}/{nextPlayer}")]
        public IActionResult PlayerPanel(bool diceRoll = false, bool nextPlayer = false)
        {
            var vm = new PlayerPanelVm
            {
                Player = _playerService.GetCurrentPlayer(),
                DiceRsult = null
            };

            if (diceRoll && !nextPlayer) 
            {
                vm.DiceRsult = _diceService.RollDice();
                vm.Player.IsDiceRolled = true;
            }
            
            if (diceRoll && nextPlayer) 
            {
                vm.Player = _playerService.GetNextPlayer();
            }

            
            return View(vm);
        }
    }
}
