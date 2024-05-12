using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface IGamePreparationService
    {
        public Game PrepareGame(int players);
    }
}
