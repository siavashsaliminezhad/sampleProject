using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public IList<OrderLineData> Lines { get; set; }

        public OrderData(Order order)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;
            CreatedAtUtc = order.CreatedAtUtc;
            Status = order.Status;
            Total = order.Total;
            Lines = order.Lines.Select(l => new OrderLineData(l)).ToList();
        }
    }
}