using System;

namespace WebApi.Models
{
    public class OrderLineModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}