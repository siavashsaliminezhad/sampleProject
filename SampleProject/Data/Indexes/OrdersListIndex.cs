using System.Linq;
using BusinessEntities;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class OrdersListIndex : AbstractIndexCreationTask<Order>
    {
        public OrdersListIndex()
        {
            Map = orders => from o in orders
                select new
                {
                    o.Status,
                    o.CustomerId,
                    o.CreatedAtUtc
                };
        }
    }
}