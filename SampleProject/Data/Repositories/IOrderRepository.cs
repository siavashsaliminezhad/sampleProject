using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Get(Guid id);
        IEnumerable<Order> Get(OrderStatus? status = null, Guid? customerId = null);
        void Save(Order order);
        void Delete(Guid id);
    }
}