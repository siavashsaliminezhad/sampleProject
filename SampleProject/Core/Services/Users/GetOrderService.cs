using System;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _repo;
        public GetOrderService(IOrderRepository repo) { _repo = repo; }
        public Order Get(Guid id) => _repo.Get(id);
    }
}