using Microsoft.AspNetCore.Mvc;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Controllers
{
    public class GameController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IDiceService _diceService;
        private readonly IAnimalService _animalService;
        private readonly ITradeOffersDataService _tradeOffersDataService;

        public GameController(IPlayerService playerService, IDiceService diceService, IAnimalService animalService, ITradeOffersDataService tradeOffersDataService)
        {
            _playerService = playerService;
            _diceService = diceService;
            _animalService = animalService;
            _tradeOffersDataService = tradeOffersDataService;
        }

        [HttpGet]
        [Route("Game/PlayerPanel/{diceRoll}/{nextPlayer}")]
        public IActionResult PlayerPanel(bool diceRoll = false, bool nextPlayer = false)
        {
            var vm = new PlayerPanelVm
            {
                Player = _playerService.GetCurrentPlayer(),
                DiceRsult = null,
                TradeOffers = null
            };

            if (diceRoll && !nextPlayer) 
            {
                vm.DiceRsult = _diceService.RollDice();
                _animalService.HandleDiceRoll(vm.DiceRsult);
                vm.Player = _playerService.GetCurrentPlayer();
                vm.Player.IsDiceRolled = true;
            }
            
            if (!diceRoll && nextPlayer) 
            {
                vm.Player = _playerService.GetNextPlayer();
            }

            vm.TradeOffers = _tradeOffersDataService.CanPlayerTrade(vm.Player);


            return View(vm);
        }
    }
}
