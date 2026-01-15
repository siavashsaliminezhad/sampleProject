using System;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderLineData
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

        public OrderLineData(OrderLine line)
        {
            ProductId = line.ProductId;
            ProductName = line.ProductName;
            UnitPrice = line.UnitPrice;
            Quantity = line.Quantity;
            LineTotal = line.LineTotal;
        }
    }
}