using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IIdObjectFactory<Product> _factory;
        private readonly IProductRepository _repo;
        private readonly IUpdateProductService _update;

        public CreateProductService(IIdObjectFactory<Product> factory, IProductRepository repo, IUpdateProductService update)
        {
            _factory = factory;
            _repo = repo;
            _update = update;
        }

        public Product Create(string name, string sku, decimal price)
        {
            var product = _factory.Create(Guid.NewGuid());
            _update.Update(product, name, sku, price, true);
            _repo.Save(product);
            return product;
        }
    }
}