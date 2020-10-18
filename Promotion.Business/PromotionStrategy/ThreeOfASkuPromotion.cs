using System;
using System.Collections.Generic;
using System.Text;
using Promotion.Core.Interface;
using Promotion.Core.Domain;
using System.Linq;

namespace Promotion.Business.PromotionStrategy
{
    public class ThreeOfASkuPromotion : IPromotionStrategy
    {
        public double OfferedPrice { get; set; }

        public bool IsActive { get; set; }

        public Common.PromotionEnum Name { get { return Common.PromotionEnum.ThreeOfASkuPromotion; } }


        public void Apply(List<OrderItem> items)
        {
            var productsA = items.Where(x => x.product.SKU == "A").SingleOrDefault();

            if (productsA != null && productsA.Quantity >=3)
            {
                int quotient = (productsA.Quantity) / 3;
                int remainder = (productsA.Quantity) % 3;

                double price = (OfferedPrice * quotient) + (remainder * productsA.Price);
                productsA.DiscountedPrice = price;
                productsA.IsDiscountApplied = true;
            }
        }

    }
}
