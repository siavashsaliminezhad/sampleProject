using System;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        void Delete(Guid id);
    }
}