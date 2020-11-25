using SortedKata.BLL.Models;

namespace SortedKata.BLL.Interfaces
{
    public interface IOfferOrchestrator
    {
        bool AddOffer(ItemOffer itemOffer);
        ItemOffer GetOffer(string sku);
    }
}
