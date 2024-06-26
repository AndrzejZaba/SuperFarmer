﻿using Newtonsoft.Json;
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
                    Id = 1,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 2,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Rabbit, 6 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    CanBeExecuted = false
                },
                //2
                new TradeOffer
                {
                    Id = 3,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 4,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 1 } },
                    CanBeExecuted = false
                },
                //3
                new TradeOffer
                {
                    Id = 5,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 6,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Pig, 3 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    CanBeExecuted = false
                },
                //4
                new TradeOffer
                {
                    Id = 7,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 8,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 2 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Horse, 1 } },
                    CanBeExecuted = false
                },
                //5
                new TradeOffer
                {
                    Id = 9,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 10,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Sheep, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.SmallDog, 1 } },
                    CanBeExecuted = false
                },
                //6
                new TradeOffer
                {
                    Id = 11,
                    OfferedAnimals = new Dictionary<AnimalType, int> { { AnimalType.BigDog, 1 } },
                    RequestedAnimals = new Dictionary<AnimalType, int> { { AnimalType.Cow, 1 } },
                    CanBeExecuted = false
                },
                new TradeOffer
                {
                    Id = 12,
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

        public TradeOffer GetTardeOfferById(int id)
        {
            var tradeOffers = GetTardeOffers();
            return tradeOffers.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<TradeOffer> CanPlayerTrade(Player player)
        {
            var tradeOffers = GetTardeOffers();
            var game = _gameDataService.GetGameData();

            foreach (var offer in tradeOffers)
            {

                var requestedAnimalsPlayerHas = player.Animals[offer.RequestedAnimals.Keys.FirstOrDefault()];
                var requestedAnimals = offer.RequestedAnimals.Values.FirstOrDefault();

                var offeredAnimals = offer.OfferedAnimals.Values.FirstOrDefault();
                var offeredAnimalsLeftInHerd = game.AllAnimalsInHerd[offer.OfferedAnimals.Keys.FirstOrDefault()];

                if (player.IsTradeDone || player.IsDiceRolled ||
                    requestedAnimalsPlayerHas < requestedAnimals ||
                    offeredAnimals > offeredAnimalsLeftInHerd)
                {

                    offer.CanBeExecuted = false;
                }
                else
                {

                    if ((offer.OfferedAnimals.Keys.FirstOrDefault() == AnimalType.SmallDog ||
                        offer.OfferedAnimals.Keys.FirstOrDefault() == AnimalType.BigDog) && 
                        player.Animals[offer.OfferedAnimals.Keys.FirstOrDefault()] >= 1)
                    {
                        offer.CanBeExecuted = false;
                    }
                    else
                    {
                        offer.CanBeExecuted = true;
                    }
                }
            }

            return tradeOffers;

        }
    }
}
