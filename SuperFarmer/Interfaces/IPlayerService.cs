using SuperFarmer.Enums;
using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface IPlayerService
    {
        Player GetCurrentPlayer();
        Player GetNextPlayer();
        bool HasPlayerWon(Player player);
    }
}
