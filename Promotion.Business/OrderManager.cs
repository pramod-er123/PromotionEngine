using Promotion.Core.Domain;
using Promotion.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Business
{
    public class OrderManager
    {
        private List<IPromotionStrategy> _promotions { get; set; }
        private Order _order { get; set; }

        /// <summary>
        /// can make configurable to add/remove promotion
        /// </summary>
        private Boolean _promotionApplicable { get; set; }

        public OrderManager(Order order, List<IPromotionStrategy> promotions, Boolean promotionApplicable)
        {
            _order = order;
            _promotions = promotions;
            _promotionApplicable = promotionApplicable;
        }

        public Order CalculateTotal()
        {
            try
            {
                double totalAmount = 0;
                double discountedAmount = 0;

                if (_promotionApplicable)
                {
                    foreach (IPromotionStrategy promotion in _promotions)
                        promotion.Apply(_order.Items);
                }

                foreach (var item in _order.Items)
                {
                    totalAmount += item.ActualPrice;
                    discountedAmount += item.IsDiscountApplied == true ? item.DiscountedPrice : item.ActualPrice;
                }

                _order.TotalAmount = totalAmount;
                _order.DiscountedAmount = discountedAmount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _order;
        }

    }
}
