using System;
using System.Collections.Generic;
using System.Text;
using Promotion.Core.Interface;
using Promotion.Core.Domain;
using System.Linq;

namespace Promotion.Business.PromotionStrategy
{
    public class TwoOfBSkuPromotion : IPromotionStrategy
    {
        public double OfferedPrice { get; set; }
        public bool IsActive { get; set; }
        public Common.PromotionEnum Name { get { return Common.PromotionEnum.TwoOfBSkuPromotion; } }

        public void Apply(List<OrderItem> items)
        {
            var productsB = items.Where(x => x.product.SKU == "B").SingleOrDefault();

            if (productsB != null && productsB.Quantity >= 2)
            {
                int quotient = (productsB.Quantity) / 2;
                int remainder = (productsB.Quantity) % 2;

                double price = (OfferedPrice * quotient) + (remainder * productsB.Price);
                productsB.DiscountedPrice = price;
                productsB.IsDiscountApplied = true;
            }
        }

    }
}
