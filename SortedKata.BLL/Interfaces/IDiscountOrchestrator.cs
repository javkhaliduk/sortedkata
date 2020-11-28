using SortedKata.BLL.Models;
using System.Collections.Generic;

namespace SortedKata.BLL.Interfaces
{
    public interface IDiscountOrchestrator
    {
        decimal CalculateDiscount(List<Item> items, ItemOffer offer);
    }
}
