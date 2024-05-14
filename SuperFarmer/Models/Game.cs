using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Game
    {
        public int Id { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int CurrentPlayerId { get; set; }
        public GameState State { get; set; }

        /// <summary>
        /// Property describes all available animals in herd for trade and reproduction. If some 
        /// animal has value 0 in herd it won't be created more of animal that kind untill some 
        /// player loses his animals or tarde them. 
        /// </summary>
        public Dictionary<AnimalType, int> AllAnimalsInHerd { get; set; } = new Dictionary<AnimalType, int>
        {
            { AnimalType.Rabbit, 60},
            { AnimalType.Sheep, 24},
            { AnimalType.Pig, 20},
            { AnimalType.Cow, 12},
            { AnimalType.Horse, 6},
            { AnimalType.SmallDog, 4},
            { AnimalType.BigDog, 2}
        };
    }
}
