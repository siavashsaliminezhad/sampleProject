using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public Order Get(Guid id)
        {
            return _documentSession.Load<Order>($"orders/{id}");
        }

        public void Save(Order order)
        {
            _documentSession.Store(order, $"orders/{order.Id}");
        }

        public void Delete(Guid id)
        {
            _documentSession.Delete($"orders/{id}");
        }

        public IEnumerable<Order> Get(OrderStatus? status = null, Guid? customerId = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrdersListIndex>();
            var hasFirst = false;

            if (status != null)
            {
                query = hasFirst ? query.AndAlso() : query;
                query = query.WhereEquals("Status", (int)status.Value);
                hasFirst = true;
            }

            if (customerId != null)
            {
                query = hasFirst ? query.AndAlso() : query;
                query = query.WhereEquals("CustomerId", customerId.Value);
                hasFirst = true;
            }

            query = query.AddOrder("CreatedAtUtc", descending: true);

            return query.ToList();
        }
    }
}