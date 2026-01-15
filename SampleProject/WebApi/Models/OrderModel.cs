using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid? CustomerId { get; set; }
        public OrderStatus? Status { get; set; } // for update
        public IEnumerable<OrderLineModel> Lines { get; set; }
    }
}