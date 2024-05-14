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

        public TradeOffersDataService(ILogger<TradeOffersDataService> logger)
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            _directoryPath = Path.Combine(projectDirectory, "DataFiles");
            _filePath = Path.Combine(_directoryPath, "tardeOffers.json");
            _logger = logger;
        }

        public void CreateTradeOffers()
        {
            var tradeOffers = new List<TradeOffer>
            {
                //1
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } }
                },
                //2
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } }
                },
                //3
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } }
                },
                //4
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } }
                },
                //5
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } }
                },
                //6
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.BigDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } }
                },
                new TradeOffer
                {
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.BigDog, 1 } }
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
    }
}
