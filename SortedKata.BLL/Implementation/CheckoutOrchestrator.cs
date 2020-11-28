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
        IDiscountOrchestrator _discountOrchestrator;
        public List<Checkout> _listCheckout;
        public CheckoutOrchestrator(IItemOrchestrator itemOrchestrator,
            IOfferOrchestrator offerOrchestrator,IDiscountOrchestrator discountOrchestrator)
        {
            _itemorchestrator = itemOrchestrator;
            _offerOrchestrator = offerOrchestrator;
            _listCheckout = new List<Checkout>();
            _discountOrchestrator = discountOrchestrator;
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
            var items = _listCheckout.First().Items.Where(p=>p.SKU==sku).ToList();
            return _discountOrchestrator.CalculateDiscount(items, offer);
        }

        public decimal GetTotalPriceWithDiscount(Guid id)
        {
            var totalPrice= GetTotalPrice(id);
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

        public decimal GetTotalPrice(Guid id)
        {
            return  _listCheckout.First(p => p.Id == id).Items.Sum(p => p.Price);
        }
    }
}
