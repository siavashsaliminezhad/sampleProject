using System;
using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public ProductData(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            Sku = p.Sku;
            Price = p.Price;
            IsActive = p.IsActive;
        }
    }
}