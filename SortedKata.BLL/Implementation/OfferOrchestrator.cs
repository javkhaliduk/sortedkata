using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.BLL.Implementation
{
    public class OfferOrchestrator : IOfferOrchestrator
    {
        public List<ItemOffer> itemOffers;
        public OfferOrchestrator()
        {
            itemOffers = new List<ItemOffer>();
        }
        public bool AddOffer(ItemOffer itemOffer)
        {
            if (itemOffer is null)
                throw new ArgumentNullException();
            else
                itemOffers.Add(itemOffer);
            return true;
        }

        public ItemOffer GetOffer(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException();
            return itemOffers.FirstOrDefault(p => p.SKU == sku);
        }
    }
}
