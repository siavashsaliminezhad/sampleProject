using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public Order()
        {
            Lines = new List<OrderLine>();
        }

        public Order(Guid id) : this()
        {
            if (id == Guid.Empty) throw new ArgumentException("Id is required.", nameof(id));
            Id = id;
            CreatedAtUtc = DateTime.UtcNow;
            Status = OrderStatus.Placed;
        }

        public Guid Id { get; private set; }
        public Guid? CustomerId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public OrderStatus Status { get; private set; }
        public IList<OrderLine> Lines { get; private set; }

        public decimal Total => Lines?.Sum(l => l.LineTotal) ?? 0m;

        public void SetCustomerId(Guid? customerId) => CustomerId = customerId;

        public void SetStatus(OrderStatus status) => Status = status;

        public void SetLines(IEnumerable<OrderLine> lines)
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            var list = lines.ToList();
            if (!list.Any()) throw new ArgumentException("Order must have at least one line.");
            Lines = list;
        }
    }
}