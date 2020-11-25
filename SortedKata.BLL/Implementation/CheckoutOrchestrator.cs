using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.BLL.Implementation
{
    public class CheckoutOrchestrator : ICheckoutOrchestrator
    {
        IItemOrchestrator _itemorchestrator;
        IOfferOrchestrator _offerOrchestrator;
        public List<Checkout> _listCheckout;
        public CheckoutOrchestrator(IItemOrchestrator itemOrchestrator,IOfferOrchestrator offerOrchestrator)
        {
            _itemorchestrator = itemOrchestrator;
            _offerOrchestrator = offerOrchestrator;
            _listCheckout = new List<Checkout>();
        }
        public decimal CalculateDiscount(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException(sku);
            var offer= _offerOrchestrator.GetOffer(sku);
            var items=_listCheckout.First().Items;
            var itemQuantity = items.Count(p => p.SKU == sku);
            var totalDiscount = 0.0m;
            if (offer != null)
            {
                var modulas = itemQuantity % offer.Quantity;
                itemQuantity = modulas != 0 ? itemQuantity - modulas : itemQuantity;
                totalDiscount=(itemQuantity / offer.Quantity) * offer.OfferPrice;
            }
            return totalDiscount;
        }

        public decimal GetTotalPrice()
        {
            return _listCheckout.First().Items.Sum(p => p.Price);
        }

        public bool ScanItem(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException(sku);
            else
            {
                var item = _itemorchestrator.GetItem(sku);
                var items = _listCheckout.FirstOrDefault()?.Items ?? new List<Item>();
                items.Add(item);
                _listCheckout.Add(new Checkout { Id = Guid.NewGuid(), Items = items });
                return true;
            }
        }

    }
}
