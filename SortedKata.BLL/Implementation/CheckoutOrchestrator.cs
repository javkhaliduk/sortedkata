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
            if (offer == null)
            {
                return 0.0m;
            }
            var items = _listCheckout.First().Items;
            var itemQuantity = items.Count(p => p.SKU == sku);
            var totalPrice = items.Where(p => p.SKU == sku).Sum(p => p.Price);
            var unitPrice = items.FirstOrDefault(p => p.SKU == sku).Price;
            var totalDiscount = 0.0m;
            var modulas = itemQuantity % offer.Quantity;
            itemQuantity = modulas != 0 ? itemQuantity - modulas : itemQuantity;
            totalDiscount = (itemQuantity / offer.Quantity) * offer.OfferPrice;
            return (totalPrice - totalDiscount) - (modulas * unitPrice);
        }

        public decimal GetTotalPrice(Guid id)
        {
            var totalPrice= _listCheckout.First(p=>p.Id==id).Items.Sum(p => p.Price);
            var items = _listCheckout.First(p => p.Id == id).Items.Select(p=>p.SKU).Distinct();
            var totalDiscount = 0.0m;

            foreach (var item in items)
            {
                totalDiscount += CalculateDiscount(item);
            }
            return totalPrice- totalDiscount;
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

        public Checkout GetAllCheckoutItems(Guid id)
        {
            return _listCheckout.FirstOrDefault(p => p.Id == id);
        }
    }
}
