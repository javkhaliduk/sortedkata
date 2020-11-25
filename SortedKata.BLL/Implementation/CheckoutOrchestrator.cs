using SortedKata.BLL.Interfaces;
using System;
namespace SortedKata.BLL.Implementation
{
    public class CheckoutOrchestrator : ICheckoutOrchestrator
    {
        public decimal CalculateDiscount(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException(sku);
            return 0;
        }

        public decimal GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public bool ScanItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException(itemId);
            return false;
        }
    }
}
