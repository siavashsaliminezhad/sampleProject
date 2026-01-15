using System;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _repo;

        public DeleteProductService(IProductRepository repo) { _repo = repo; }

        public void Delete(Guid id) => _repo.Delete(id);
    }
}