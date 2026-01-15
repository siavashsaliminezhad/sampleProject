using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string sku, decimal? price, bool? isActive)
        {
            if (name != null) product.SetName(name);
            if (sku != null) product.SetSku(sku);
            if (price != null) product.SetPrice(price.Value);

            if (isActive != null)
            {
                if (isActive.Value) product.Activate();
                else product.Deactivate();
            }
        }
    }
}