using Promotion.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Core.Interface
{
    public interface IPromotionStrategy
    {
        double offeredPrice { get; set; }
        Common.PromotionEnum name { get; }

        bool isActive { get; set; }
        void Apply(List<OrderItem> items);

    }
}
