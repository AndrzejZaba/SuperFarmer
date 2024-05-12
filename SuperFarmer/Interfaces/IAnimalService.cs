using SuperFarmer.Enums;
using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface IAnimalService
    {
        void HandleDiceRoll(IList<AnimalType> diceResult);
        void HandleTrade(Player player);
    }
}
