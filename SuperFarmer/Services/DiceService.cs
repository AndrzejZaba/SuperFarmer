using SuperFarmer.Enums;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class DiceService : IDiceService
    {
        public Dice Dice1 { get; set; } 
        public Dice Dice2 { get; set; }
        public DiceService()
        {
            Dice1 = new Dice();
            Dice2 = new Dice();

            Dice1.Faces.AddRange(Enumerable.Repeat(AnimalType.Rabbit, 6));
            Dice1.Faces.AddRange(Enumerable.Repeat(AnimalType.Sheep, 3));
            Dice1.Faces.Add(AnimalType.Pig);
            Dice1.Faces.Add(AnimalType.Cow);
            Dice1.Faces.Add(AnimalType.Wolf);
            
            Dice2.Faces.AddRange(Enumerable.Repeat(AnimalType.Rabbit, 6));
            Dice2.Faces.AddRange(Enumerable.Repeat(AnimalType.Sheep, 2));
            Dice2.Faces.AddRange(Enumerable.Repeat(AnimalType.Pig, 2));
            Dice2.Faces.Add(AnimalType.Horse);
            Dice2.Faces.Add(AnimalType.Fox);
        }
        public List<AnimalType> RollDice()
        {
            Random rand = new Random();
            int index1 = rand.Next(Dice1.Faces.Count);
            int index2 = rand.Next(Dice2.Faces.Count);

            return new List<AnimalType> { Dice1.Faces[index1], Dice2.Faces[index2] };
        }
    }
}
