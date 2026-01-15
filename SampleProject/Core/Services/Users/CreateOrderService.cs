using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _factory;
        private readonly IOrderRepository _repo;
        private readonly IUpdateOrderService _update;

        public CreateOrderService(IIdObjectFactory<Order> factory, IOrderRepository repo, IUpdateOrderService update)
        {
            _factory = factory;
            _repo = repo;
            _update = update;
        }

        public Order Create(Guid? customerId, IEnumerable<(Guid productId, int quantity)> lines)
        {
            var order = _factory.Create(Guid.NewGuid());
            _update.Update(order, customerId, OrderStatus.Placed, lines);
            _repo.Save(order);
            return order;
        }
    }
}