using System;
using System.Collections.Generic;
using System.Text;
using Promotion.Core.Interface;
using Promotion.Core.Domain;
using System.Linq;

namespace Promotion.Business.PromotionStrategy
{
    class ThreeOfASkuPromotion : IPromotionStrategy
    {
        public double offeredPrice { get; set; }

        public bool isActive { get; set; }

        public Common.PromotionEnum name { get { return Common.PromotionEnum.ThreeOfASkuPromotion; } }
        

        public void Apply(List<OrderItem> items)
        {
            var productsA = items.Where(x => x.product.SKU == "A").SingleOrDefault();

            if (productsA != null)
            {
                int quotient = (productsA.Quantity) / 3;
                int remainder = (productsA.Quantity) % 3;

                double price = (offeredPrice * quotient) + (remainder * productsA.Price);
                productsA.DiscountedPrice = price;
                productsA.IsDiscountApplied = true;
            }
        }

    }
}
