using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>
        {
            new Animal
            {
                Id = 0,
                AnimalType = AnimalType.Rabbit,
                Amount = 1,
            },
            new Animal
            {
                Id = 1,
                AnimalType = AnimalType.Sheep,
                Amount = 0,
            },
            new Animal
            {
                Id = 2,
                AnimalType = AnimalType.Pig,
                Amount = 0,
            },
            new Animal
            {
                Id = 3,
                AnimalType = AnimalType.Cow,
                Amount = 0,
            },
            new Animal
            {
                Id = 4,
                AnimalType = AnimalType.Horse,
                Amount = 0,
            },
            new Animal
            {
                Id = 5,
                AnimalType = AnimalType.SmallDog,
                Amount = 0,
            },
            new Animal
            {
                Id = 6,
                AnimalType = AnimalType.BigDog,
                Amount = 0,
            }

        };
    }
}
