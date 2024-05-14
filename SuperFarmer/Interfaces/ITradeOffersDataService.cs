using SuperFarmer.Models;

namespace SuperFarmer.Interfaces
{
    public interface ITradeOffersDataService
    {
        List<TradeOffer> GetTardeOffers();
        TradeOffer GetTardeOfferById(int id);
        void CreateTradeOffers();
        List<TradeOffer> CanPlayerTrade(Player player);
    }
}
