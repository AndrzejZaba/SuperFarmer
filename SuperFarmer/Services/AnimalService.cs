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
            var game = _gameDataService.GetGameData();

            if (diceResult[0] == diceResult[1])
            {
                var newAnimals = (player.Animals[diceResult[0]] + 2) / 2;

                if (game.AllAnimalsInHerd[diceResult[0]] < newAnimals)
                    newAnimals = game.AllAnimalsInHerd[diceResult[0]];
                   
                player.Animals[diceResult[0]] += newAnimals;
                game.AllAnimalsInHerd[diceResult[0]] -= newAnimals;


            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (diceResult[i] != AnimalType.Fox && diceResult[i] != AnimalType.Wolf)
                    {
                        var newAnimals = (player.Animals[diceResult[i]] + 1) / 2;

                        if (game.AllAnimalsInHerd[diceResult[i]] < newAnimals)
                            newAnimals = game.AllAnimalsInHerd[diceResult[i]];

                        player.Animals[diceResult[i]] += newAnimals;
                        game.AllAnimalsInHerd[diceResult[i]] -= newAnimals;

                    }
                }
            }
            
            if (diceResult.Contains(AnimalType.Fox))
            {
                if (player.Animals[AnimalType.SmallDog] == 1)
                {
                    player.Animals[AnimalType.SmallDog] = 0;
                    game.AllAnimalsInHerd[AnimalType.SmallDog]++;
                }
                else
                {
                    var lostRabbits = player.Animals[AnimalType.Rabbit] - 1;
                    player.Animals[AnimalType.Rabbit] = 1;
                    game.AllAnimalsInHerd[AnimalType.Rabbit] += lostRabbits;
                }
            }
            
            if (diceResult.Contains(AnimalType.Wolf))
            {
                if (player.Animals[AnimalType.BigDog] == 1)
                {
                    player.Animals[AnimalType.BigDog] = 0;
                    game.AllAnimalsInHerd[AnimalType.BigDog]++;
                }
                else
                {
                    var lostSheep = player.Animals[AnimalType.Sheep];
                    var lostPigs = player.Animals[AnimalType.Pig];
                    var lostCows = player.Animals[AnimalType.Cow];
                    player.Animals[AnimalType.Sheep] = 0;
                    player.Animals[AnimalType.Pig] = 0;
                    player.Animals[AnimalType.Cow] = 0;
                    game.AllAnimalsInHerd[AnimalType.Sheep] += lostSheep;
                    game.AllAnimalsInHerd[AnimalType.Pig] += lostPigs;
                    game.AllAnimalsInHerd[AnimalType.Cow] += lostCows;
                }
                
            }

            
            game.Players[player.Id-1] = player;
            _gameDataService.SaveGameData(game);
        }

        public void HandleTrade(Player player, TradeOffer tradeOffer)
        {
            //if (tradeOffer.CanBeExecuted)
            //{

                var game = _gameDataService.GetGameData();

                var requestedAnimal = tradeOffer.RequestedAnimals.Keys.FirstOrDefault();
                var requestedAnimalsNumber = tradeOffer.RequestedAnimals.Values.FirstOrDefault();
                
                var offeredAnimal = tradeOffer.OfferedAnimals.Keys.FirstOrDefault();
                var offeredAnimalsNumber = tradeOffer.OfferedAnimals.Values.FirstOrDefault();

                if (player.Animals[requestedAnimal] >= requestedAnimalsNumber &&
                    offeredAnimalsNumber <= game.AllAnimalsInHerd[offeredAnimal])
                {
                    player.Animals[requestedAnimal] -= requestedAnimalsNumber;
                    game.AllAnimalsInHerd[requestedAnimal] += requestedAnimalsNumber;

                    player.Animals[offeredAnimal] += offeredAnimalsNumber;
                    game.AllAnimalsInHerd[offeredAnimal] -= offeredAnimalsNumber;

                    player.IsTradeDone = true;
                }

                game.Players[player.Id - 1] = player;
                _gameDataService.SaveGameData(game);

            }
        //}
    }
}
