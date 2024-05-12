using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class Game
    {
        public int Id { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int CurrentPlayerId { get; set; }
        public GameState State { get; set; }
    }
}
