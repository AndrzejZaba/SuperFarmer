using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<AnimalType, int> Animals { get; set; } = new Dictionary<AnimalType, int>
        {
            { AnimalType.Rabbit, 0},
            { AnimalType.Sheep, 12},
            { AnimalType.Pig, 1},
            { AnimalType.Cow, 1},
            { AnimalType.Horse, 1},
            { AnimalType.SmallDog, 1},
            { AnimalType.BigDog, 1}
        };
        public bool IsTradeDone { get; set; }
        public bool IsDiceRolled { get; set; }
    }
}
