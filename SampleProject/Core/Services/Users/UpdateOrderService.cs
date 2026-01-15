using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class UpdateOrderService : IUpdateOrderService
    {
        private readonly IProductRepository _productRepo;

        public UpdateOrderService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public void Update(Order order, Guid? customerId, OrderStatus? status, IEnumerable<(Guid productId, int quantity)> lines)
        {
            if (customerId != null) order.SetCustomerId(customerId);
            if (status != null) order.SetStatus(status.Value);

            if (lines != null)
            {
                var list = lines.ToList();
                if (!list.Any()) throw new ArgumentException("Order must have at least one line.");

                var orderLines = new List<OrderLine>();

                foreach (var (productId, quantity) in list)
                {
                    if (productId == Guid.Empty) throw new ArgumentException("ProductId is required.");
                    if (quantity <= 0) throw new ArgumentException("Quantity must be > 0.");

                    var product = _productRepo.Get(productId);
                    if (product == null) throw new ArgumentException($"Product not found: {productId}");
                    if (!product.IsActive) throw new ArgumentException($"Product inactive: {productId}");

                    orderLines.Add(new OrderLine(product.Id, product.Name, product.Price, quantity));
                }

                order.SetLines(orderLines);
            }
        }
    }
}