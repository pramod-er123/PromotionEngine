using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Core.Domain
{
    public class OrderItem
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double ActualPrice
        {
            get
            { return (Quantity * Price); }
        }
        public double DiscountedPrice { get; set; }
        public bool IsDiscountApplied { get; set; }
    }
}
