using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models
{
    public class OrderModel
    {
        public Guid? CustomerId { get; set; }
        public OrderStatus? Status { get; set; } // optional for update
        public IEnumerable<OrderLineModel> Lines { get; set; }
    }
}