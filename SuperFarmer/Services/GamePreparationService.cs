﻿using SuperFarmer.Enums;
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
                CurrentPlayerId = 1,
                State = GameState.Started
            };

            for (int i = 1; i <= players; i++)
            {
                game.Players.Add(new Player
                {
                    Id = i,
                    Name = "Gracz " + i.ToString(),
                    IsDiceRolled = false,
                    IsTradeDone = false,
                });
                game.AllAnimalsInHerd[AnimalType.Rabbit] -= 6;
                game.AllAnimalsInHerd[AnimalType.Sheep] -= 12;
            }

            _gameDataService.SaveGameData(game);

            return game;
        }
    }
}
