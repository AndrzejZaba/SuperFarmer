using Newtonsoft.Json;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class GameDataService : IGameDataService
    {
        public Game GetGameData()
        {
            throw new NotImplementedException();
        }

        public void SaveGameData(Game game)
        {
            var json = JsonConvert.SerializeObject(game);

            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string directoryPath = Path.Combine(projectDirectory, "DataFiles");
            string filePath = Path.Combine(directoryPath, "gameData.json");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            File.WriteAllText(filePath, json);
        }
    }
}
