
using SortedKata.BLL.Models;
using System;

namespace SortedKata.BLL.Interfaces
{
    public interface ICheckoutOrchestrator
    {
        bool ScanItem(string sku);
        decimal GetTotalPrice(Guid id);
        decimal GetTotalPriceWithDiscount(Guid id);
        decimal CalculateDiscount(string sku);
        Checkout GetAllCheckoutItems(Guid id);
    }
}
