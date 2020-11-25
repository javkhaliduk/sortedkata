using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.BLL.Implementation
{
    public class OfferOrchestrator : IOfferOrchestrator
    {
        public List<ItemOffer> itemOffers = new List<ItemOffer>();
        public OfferOrchestrator()
        {
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
        private void PopulateItems()
        {
            itemOffers = new List<ItemOffer> {
            new ItemOffer{SKU="A99", Quantity=3, OfferPrice=1.30m },
            new ItemOffer{ SKU="B15", Quantity=2, OfferPrice=0.45m }
            };
        }
    }
}
