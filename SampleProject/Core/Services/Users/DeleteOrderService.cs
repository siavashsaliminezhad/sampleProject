using System;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _repo;
        public DeleteOrderService(IOrderRepository repo) { _repo = repo; }
        public void Delete(Guid id) => _repo.Delete(id);
    }
}