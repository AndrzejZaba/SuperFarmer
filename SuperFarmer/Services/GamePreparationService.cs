using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class GamePreparationService : IGamePreparationService
    {
        private readonly IGameDataService _gameDataService;
        private readonly ITradeOffersDataService _tradeOffersDataService;

        public GamePreparationService(
            IGameDataService gameDataService, 
            ITradeOffersDataService tradeOffersDataService)
        {
            _gameDataService = gameDataService;
            _tradeOffersDataService = tradeOffersDataService;
        }
        public Game PrepareGame(int players)
        {
            var game = new Game
            {
                Id = 1,
                CurrentPlayerId = 1
            };

            for (int i = 1; i <= players; i++)
            {
                game.Players.Add(new Player
                {
                    Id = i,
                    Name = "Player " + i.ToString(),
                    IsDiceRolled = false,
                    IsTradeDone = false,
                });
                game.AllAnimalsInHerd[AnimalType.Rabbit] -= 1;
            }

            _gameDataService.SaveGameData(game);

            return game;
        }
    }
}
