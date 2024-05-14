﻿using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class TradeOffer
    {
        public Dictionary<AnimalType, int> OfferedAnimals { get; set; }
        public Dictionary<AnimalType, int> RequestedAnimals { get; set; }
        public bool CanBeExecuted { get; set; }
    }
}
