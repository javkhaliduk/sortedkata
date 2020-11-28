using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.BLL.Implementation
{
    public class DiscountOrchestrator : IDiscountOrchestrator
    {
        public decimal CalculateDiscount(List<Item> items,ItemOffer offer)
        {
            if (items == null || items.Count == 0)
                return 0.0m;
            var itemQuantity = items.Count();
            var totalPrice = items.Sum(p => p.Price);
            var unitPrice = items.FirstOrDefault().Price;
            var totalDiscount = 0.0m;
            var modulas = itemQuantity % offer.Quantity;
            itemQuantity = modulas != 0 ? itemQuantity - modulas : itemQuantity;
            totalDiscount = (itemQuantity / offer.Quantity) * offer.OfferPrice;
            return (totalPrice - totalDiscount) - (modulas * unitPrice);
        }
    }
}
