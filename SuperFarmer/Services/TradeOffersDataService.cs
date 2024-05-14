using Newtonsoft.Json;
using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class TradeOffersDataService : ITradeOffersDataService
    {
        public readonly string _filePath;
        public readonly string _directoryPath;
        private readonly ILogger<TradeOffersDataService> _logger;
        private readonly IGameDataService _gameDataService;

        public TradeOffersDataService(ILogger<TradeOffersDataService> logger,
            IGameDataService gameDataService)
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            _directoryPath = Path.Combine(projectDirectory, "DataFiles");
            _filePath = Path.Combine(_directoryPath, "tardeOffers.json");
            _logger = logger;
            _gameDataService = gameDataService;
        }

        public void CreateTradeOffers()
        {
            var tradeOffers = new List<TradeOffer>
            {
                //1
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    CanBeExecuted = false
                },
                //2
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } },
                    CanBeExecuted = false
                },
                //3
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    CanBeExecuted = false
                },
                //4
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } },
                    CanBeExecuted = false
                },
                //5
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } },
                    CanBeExecuted = false
                },
                //6
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.BigDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.BigDog, 1 } },
                    CanBeExecuted = false
                }

            };
            
            var json = JsonConvert.SerializeObject(tradeOffers);

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);
        }

        public List<TradeOffer> GetTardeOffers()
        {
            try
            {
                if (!File.Exists(_filePath) || new FileInfo("file").Length == 0)
                    throw new FileNotFoundException("Nie można wczytać ofert wymiany zwierząt.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                CreateTradeOffers();
            }
            
            string json = File.ReadAllText(_filePath);

            List<TradeOffer> tradeOffers = JsonConvert.DeserializeObject<List<TradeOffer>>(json);

            return tradeOffers;
        }
        
        public List<TradeOffer> CanPlayerTrade(Player player)
        {
            var tradeOffers = GetTardeOffers();
            var game = _gameDataService.GetGameData();

            foreach (var offer in tradeOffers)
            {
                if(player.IsTradeDone || player.IsDiceRolled || 
                    player.Animals[offer.RequestedAnimals.Keys.FirstOrDefault()] < offer.RequestedAnimals.Values.FirstOrDefault() ||
                    offer.OfferedAnimals.Values.FirstOrDefault() > game.AllAnimalsInHerd[offer.OfferedAnimals.Keys.FirstOrDefault()])
                {
                    offer.CanBeExecuted = false;
                }
                else
                {
                    offer.CanBeExecuted = true;
                }
            }

            return tradeOffers;

        }
    }
}
