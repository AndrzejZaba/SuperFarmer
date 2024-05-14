using Microsoft.AspNetCore.Mvc;
using SuperFarmer.Enums;
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
        [Route("Game/PlayerPanel/{diceRoll}/{nextPlayer}/{offerId}")]
        public IActionResult PlayerPanel(bool diceRoll = false, bool nextPlayer = false, int offerId = 0)
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

            if (offerId != 0)
            {
                _animalService.HandleTrade(vm.Player, _tradeOffersDataService.GetTardeOfferById(offerId));
            }

            if (_playerService.HasPlayerWon(vm.Player))
            {
                return RedirectToAction("FinalScreen", new {playerId = vm.Player.Id });
            }

            if (!diceRoll && nextPlayer) 
            {
                vm.Player = _playerService.GetNextPlayer();
            }


            vm.TradeOffers = _tradeOffersDataService.CanPlayerTrade(vm.Player);

            return View(vm);
        }

        [HttpGet]
        [Route("Game/FinalScreen/{playerId}")]
        public IActionResult FinalScreen(int playerId)
        {
            return View(playerId);
        }

    }
}
