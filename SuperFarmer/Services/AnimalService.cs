using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IPlayerService _playerService;

        public AnimalService(IGameDataService gameDataService, IPlayerService playerService)
        {
            _gameDataService = gameDataService;
            _playerService = playerService;
        }
        public void HandleDiceRoll(IList<AnimalType> diceResult)
        {
            var player = _playerService.GetCurrentPlayer();

            if(diceResult[0] == diceResult[1])
            {
                var newAnimals = (player.Animals[diceResult[0]] + 2) / 2;
                player.Animals[diceResult[0]] += newAnimals;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (diceResult[i] != AnimalType.Fox && diceResult[i] != AnimalType.Wolf)
                    {
                        var newAnimals = (player.Animals[diceResult[i]] + 1) / 2;
                        player.Animals[diceResult[i]] += newAnimals;
                    }
                }
            }
            
            if (diceResult.Contains(AnimalType.Fox))
            {
                if (player.Animals[AnimalType.SmallDog] == 1)
                    player.Animals[AnimalType.SmallDog] = 0;
                else
                    player.Animals[AnimalType.Rabbit] = 1;                
            }
            
            if (diceResult.Contains(AnimalType.Wolf))
            {
                if (player.Animals[AnimalType.BigDog] == 1)
                {
                    player.Animals[AnimalType.BigDog] = 0;
                }
                else
                {
                    player.Animals[AnimalType.Sheep] = 0;
                    player.Animals[AnimalType.Pig] = 0;
                    player.Animals[AnimalType.Cow] = 0;
                }
                
            }

            var game = _gameDataService.GetGameData();
            game.Players[player.Id-1] = player;
            _gameDataService.SaveGameData(game);
        }

        public void HandleTrade(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
