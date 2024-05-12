using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class GamePreparationService : IGamePreparationService
    {
        private readonly IGameDataService _gameDataService;

        public GamePreparationService(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
        }
        public Game PrepareGame(int players)
        {
            var game = new Game
            {
                Id = 1,
                CurrentPlayerId = 1,
                State = GameState.Started
            };

            for (int i = 0; i < players; i++)
            {
                game.Players.Add(new Player
                {
                    Id = i,
                    Name = "Gracz " + i.ToString()
                });
            }

            _gameDataService.SaveGameData(game);

            return game;
        }
    }
}
