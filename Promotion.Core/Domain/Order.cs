using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Core.Domain
{
    public class Order
    {
        public string OrderNo { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public Double TotalAmount { get; set; }
        public Double DiscountedAmount { get; set; }
    }
}
