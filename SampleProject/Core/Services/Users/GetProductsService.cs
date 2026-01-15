using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductsService : IGetProductsService
    {
        private readonly IProductRepository _repo;

        public GetProductsService(IProductRepository repo) { _repo = repo; }

        public IEnumerable<Product> Get(string sku = null, string name = null, bool? isActive = null)
            => _repo.Get(sku, name, isActive);
    }
}