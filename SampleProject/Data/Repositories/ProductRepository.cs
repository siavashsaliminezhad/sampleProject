using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public Product Get(Guid id)
        {
            return _documentSession.Load<Product>($"products/{id}");
        }

        public void Save(Product product)
        {
            _documentSession.Store(product, $"products/{product.Id}");
        }

        public void Delete(Guid id)
        {
            var product = Get(id);
            if (product == null) return;
            product.Deactivate();
            _documentSession.Store(product, $"products/{product.Id}");
        }

        public IEnumerable<Product> Get(string sku = null, string name = null, bool? isActive = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();
            var hasFirst = false;

            if (!string.IsNullOrWhiteSpace(sku))
            {
                query = hasFirst ? query.AndAlso() : query;
                query = query.WhereEquals("Sku", sku.Trim());
                hasFirst = true;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = hasFirst ? query.AndAlso() : query;
                query = query.WhereEquals("Name", name.Trim());
                hasFirst = true;
            }

            if (isActive != null)
            {
                query = hasFirst ? query.AndAlso() : query;
                query = query.WhereEquals("IsActive", isActive.Value);
                hasFirst = true;
            }

            return query.ToList();
        }
    }
}