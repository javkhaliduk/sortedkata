using System;
using System.Collections.Generic;
using System.Text;

namespace SortedKata.BLL.Models
{
    public class ItemOffer
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
    }
}
