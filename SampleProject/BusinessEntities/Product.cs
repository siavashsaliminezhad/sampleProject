using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        public Product() { } // Raven

        public Product(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id is required.", nameof(id));
            Id = id;
            CreatedAtUtc = DateTime.UtcNow;
            IsActive = true;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public string Name { get; private set; }
        public string Sku { get; private set; }
        public decimal Price { get; private set; }

        public bool IsActive { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
            Name = name.Trim();
        }

        public void SetSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("Sku is required.");
            Sku = sku.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            Price = price;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}