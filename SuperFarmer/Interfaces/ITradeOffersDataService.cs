using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface ITradeOffersDataService
    {
        List<TradeOffer> GetTardeOffers();
        void CreateTradeOffers();
    }
}
