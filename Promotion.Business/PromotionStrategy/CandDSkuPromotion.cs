using System;
using System.Collections.Generic;
using System.Text;
using Promotion.Core.Interface;
using Promotion.Core.Domain;
using System.Linq;

namespace Promotion.Business.PromotionStrategy
{
    class CandDSkuPromotion : IPromotionStrategy
    {
        public double offeredPrice { get; set; }
        public bool IsActive { get; set; }

        public Common.PromotionEnum name { get { return Common.PromotionEnum.CandDSkuPromotion; } }

        public CandDSkuPromotion(double offeredPrice)
        {
            this.offeredPrice = offeredPrice;
        }

        public void Apply(List<OrderItem> items)
        {
            double priceAfterDiscount = 0;

            var productsC = items.Where(x => x.product.SKU == "C").SingleOrDefault();
            var productsD = items.Where(x => x.product.SKU == "D").SingleOrDefault();

            if (productsC != null && productsD != null)
            {
                if (productsC.Quantity == productsD.Quantity)
                {
                    priceAfterDiscount = productsC.Quantity * offeredPrice;
                }
                else if (productsC.Quantity > productsD.Quantity)
                {
                    int diff = productsC.Quantity - productsD.Quantity;
                    priceAfterDiscount = (diff * offeredPrice) + ((productsC.Quantity - diff) * productsC.ActualPrice);

                }
                productsC.DiscountedPrice = priceAfterDiscount;
                productsD.DiscountedPrice = 0;
                productsC.IsDiscountApplied = true;
                productsD.IsDiscountApplied = true;
            }

        }
    }
}
