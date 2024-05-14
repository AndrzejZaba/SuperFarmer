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
            { AnimalType.Sheep, 10},
            { AnimalType.Pig, 0},
            { AnimalType.Cow, 10},
            { AnimalType.Horse, 0},
            { AnimalType.SmallDog, 1},
            { AnimalType.BigDog, 1}
        };
        public bool IsTradeDone { get; set; }
        public bool IsDiceRolled { get; set; }
    }
}
