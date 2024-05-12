using SuperFarmer.Enums;
using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface IPlayerService
    {
        Player GetCurrentPlayer();
        Player GetNextPlayer();
        void HandlerDiceRoll(AnimalType dice1, AnimalType dice2);
        //void HandleTrade();
    }
}
