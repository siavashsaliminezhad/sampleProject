using System;

namespace BusinessEntities
{
    public class OrderLine
    {
        public OrderLine() { } // Raven

        public OrderLine(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (productId == Guid.Empty) throw new ArgumentException("ProductId is required.");
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("ProductName is required.");
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice cannot be negative.");
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be > 0.");

            ProductId = productId;
            ProductName = productName.Trim();
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}