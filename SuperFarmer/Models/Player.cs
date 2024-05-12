using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<AnimalType, int> Animals { get; set; } = new Dictionary<AnimalType, int>
        {
            { AnimalType.Rabbit, 1},
            { AnimalType.Sheep, 0},
            { AnimalType.Pig, 0},
            { AnimalType.Cow, 0},
            { AnimalType.Horse, 0},
            { AnimalType.SmallDog, 0},
            { AnimalType.BigDog, 0}
        };
        public bool IsTradeDone { get; set; }
        public bool IsDiceRolled { get; set; }
    }
}
