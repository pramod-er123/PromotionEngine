using System;
using System.Collections.Generic;
using System.Text;
using Promotion.Core.Interface;
using Promotion.Core.Domain;
using System.Linq;

namespace Promotion.Business.PromotionStrategy
{
    public class CandDSkuPromotion : IPromotionStrategy
    {
        public double OfferedPrice { get; set; }
        public bool IsActive { get; set; }
        public Common.PromotionEnum Name { get { return Common.PromotionEnum.CandDSkuPromotion; } }

        public void Apply(List<OrderItem> items)
        {
            double priceAfterDiscount = 0;

            var productsC = items.Where(x => x.product.SKU == "C").SingleOrDefault();
            var productsD = items.Where(x => x.product.SKU == "D").SingleOrDefault();

            if (productsC != null && productsD != null && productsC.Quantity > 0 && productsD.Quantity > 0)
            {
                if (productsC.Quantity == productsD.Quantity)
                {
                    priceAfterDiscount = productsC.Quantity * OfferedPrice;
                }
                else if (productsC.Quantity > productsD.Quantity)
                {
                    int diff = productsC.Quantity - productsD.Quantity;
                    priceAfterDiscount = (diff * OfferedPrice) + ((productsC.Quantity - diff) * productsC.ActualPrice);

                }
                productsC.DiscountedPrice = priceAfterDiscount;
                productsD.DiscountedPrice = 0;
                productsC.IsDiscountApplied = true;
                productsD.IsDiscountApplied = true;
            }

        }
    }
}
