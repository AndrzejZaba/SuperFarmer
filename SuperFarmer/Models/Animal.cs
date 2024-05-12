using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public AnimalType AnimalType { get; set; }
        /// <summary>
        /// How many animals of specific type player has
        /// </summary>
        public int Amount { get; set; }
    }
}
