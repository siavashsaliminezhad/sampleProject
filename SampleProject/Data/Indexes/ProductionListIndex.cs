using System.Linq;
using BusinessEntities;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from p in products
                select new
                {
                    p.Name,
                    p.Sku,
                    p.IsActive
                };
        }
    }
}