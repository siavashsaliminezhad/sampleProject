using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid? customerId, IEnumerable<(Guid productId, int quantity)> lines);
    }
}