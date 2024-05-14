using SuperFarmer.Enums;

namespace SuperFarmer.Models
{
    public class PlayerPanelVm
    {
        public Player Player { get; set; }
        public List<AnimalType> DiceRsult { get; set; }
        public List<TradeOffer> TradeOffers { get; set;}
    }
}
