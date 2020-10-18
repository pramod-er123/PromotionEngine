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
        private double _offeredPrice { get; set; }
        public CandDSkuPromotion(double offeredPrice)
        {
            _offeredPrice = offeredPrice;
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
                    priceAfterDiscount = productsC.Quantity * _offeredPrice;
                }
                else if (productsC.Quantity > productsD.Quantity)
                {
                    int diff = productsC.Quantity - productsD.Quantity;
                    priceAfterDiscount = (diff * _offeredPrice) + ((productsC.Quantity - diff) * productsC.ActualPrice);

                }
                productsC.DiscountedPrice = priceAfterDiscount;
                productsD.DiscountedPrice = 0;
                productsC.IsDiscountApplied = true;
                productsD.IsDiscountApplied = true;
            }

        }
    }
}
