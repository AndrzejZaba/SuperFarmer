using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface IGameDataService
    {
        void SaveGameData(Game game);
        Game GetGameData();
    }
}
