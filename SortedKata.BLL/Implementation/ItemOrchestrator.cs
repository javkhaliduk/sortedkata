using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;

namespace SortedKata.BLL.Implementation
{
    public class ItemOrchestrator : IItemOrchestrator
    {
        public bool AddItem(Item item)
        {
            if (item is null)
                throw new ArgumentNullException();
            else return false;
        }

        public Item GetItem(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException();
            else return null; ;
        }
    }
}
