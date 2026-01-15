using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Get(Guid id);
        IEnumerable<Product> Get(string sku = null, string name = null, bool? isActive = null);
        void Save(Product product);
        void Delete(Guid id); // soft delete (deactivate)
    }
}