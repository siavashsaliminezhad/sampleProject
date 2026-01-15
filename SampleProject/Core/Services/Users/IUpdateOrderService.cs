using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid? customerId, OrderStatus? status, IEnumerable<(Guid productId, int quantity)> lines);
    }
}
