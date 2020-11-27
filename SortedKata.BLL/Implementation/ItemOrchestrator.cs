using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.BLL.Implementation
{
    public class ItemOrchestrator : IItemOrchestrator
    {
        public List<Item> _listItems;
        public ItemOrchestrator()
        {
            _listItems = new List<Item>();
        }
        public bool AddItem(Item item)
        {
            if (item is null)
                throw new ArgumentNullException();
            else 
                _listItems.Add(item);
            return true;
        }

        public Item GetItem(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                throw new ArgumentNullException();
            else
                return _listItems.FirstOrDefault(p => p.SKU==sku);
        }
    }
}
