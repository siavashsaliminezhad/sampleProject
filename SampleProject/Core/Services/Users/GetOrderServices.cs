using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrdersService : IGetOrdersService
    {
        private readonly IOrderRepository _repo;
        public GetOrdersService(IOrderRepository repo) { _repo = repo; }
        public IEnumerable<Order> Get(OrderStatus? status = null, Guid? customerId = null) => _repo.Get(status, customerId);
    }
}