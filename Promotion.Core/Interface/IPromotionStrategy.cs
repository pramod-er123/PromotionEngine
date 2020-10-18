using Promotion.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Core.Interface
{
    public interface IPromotionStrategy
    {
        void Apply(List<OrderItem> items);

    }
}
