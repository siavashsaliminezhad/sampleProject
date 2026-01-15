using System;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _repo;

        public GetProductService(IProductRepository repo) { _repo = repo; }

        public Product Get(Guid id) => _repo.Get(id);
    }
}