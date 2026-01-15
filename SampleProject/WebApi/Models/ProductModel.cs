namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}