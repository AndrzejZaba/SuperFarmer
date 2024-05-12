using Newtonsoft.Json;
using SuperFarmer.Interfaces;
using SuperFarmer.Models;

namespace SuperFarmer.Services
{
    public class GameDataService : IGameDataService
    {
        public readonly string _filePath;
        public readonly string _directoryPath;
        public GameDataService()
        {
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            _directoryPath = Path.Combine(projectDirectory, "DataFiles");
            _filePath = Path.Combine(_directoryPath, "gameData.json");
        }
        public Game GetGameData()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Nie znaleziono pliku z danymi gry.");

            string json = File.ReadAllText(_filePath);

            Game gameData = JsonConvert.DeserializeObject<Game>(json);

            return gameData;
        }

        public void SaveGameData(Game game)
        {
            var json = JsonConvert.SerializeObject(game);

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);
        }
    }
}
