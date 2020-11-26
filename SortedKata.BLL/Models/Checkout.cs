using System;
using System.Collections.Generic;
using System.Text;

namespace SortedKata.BLL.Models
{
    public class Checkout
    {
        public Guid Id { get; set; }
        public List<Item> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public Checkout()
        {
            Items = new List<Item>();
        }
    }
}
