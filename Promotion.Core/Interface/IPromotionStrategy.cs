using Promotion.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Core.Interface
{
    public interface IPromotionStrategy
    {
        double OfferedPrice { get; set; }
        Common.PromotionEnum Name { get; }

        bool IsActive { get; set; }
        void Apply(List<OrderItem> items);

    }
}
