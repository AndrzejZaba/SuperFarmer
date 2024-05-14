using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IGameDataService _gameDataService;

        public PlayerService(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
        }

        public Player GetCurrentPlayer()
        {
            var game = _gameDataService.GetGameData();

            return game.Players.Where(x => x.Id == game.CurrentPlayerId).FirstOrDefault();
        }

        public Player GetNextPlayer()
        {
            var game = _gameDataService.GetGameData();

            game.Players.Where(x => x.Id == game.CurrentPlayerId).FirstOrDefault().IsTradeDone = false;

            if (game.CurrentPlayerId < game.Players.Count)
            {
                game.CurrentPlayerId++;
            }
            else
            {
                game.CurrentPlayerId = 1;
            }

            _gameDataService.SaveGameData(game);

            return game.Players.Where(x => x.Id == game.CurrentPlayerId).FirstOrDefault();
        }

        public bool HasPlayerWon(Player player)
        {
            if (player.Animals[AnimalType.Rabbit] >= 1 &&
                player.Animals[AnimalType.Sheep] >= 1 &&
                player.Animals[AnimalType.Pig] >= 1 &&
                player.Animals[AnimalType.Cow] >= 1 &&
                player.Animals[AnimalType.Horse] >= 1)
                return true;
            else
                return false;
        }
    }
}
