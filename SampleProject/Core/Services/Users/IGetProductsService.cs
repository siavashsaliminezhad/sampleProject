using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IGetProductsService
    {
        IEnumerable<Product> Get(string sku = null, string name = null, bool? isActive = null);
    }
}