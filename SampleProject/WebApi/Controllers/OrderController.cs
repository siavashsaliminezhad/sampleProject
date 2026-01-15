using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _create;
        private readonly IUpdateOrderService _update;
        private readonly IGetOrderService _get;
        private readonly IGetOrdersService _list;
        private readonly IDeleteOrderService _delete;

        public OrderController(
            ICreateOrderService create,
            IUpdateOrderService update,
            IGetOrderService get,
            IGetOrdersService list,
            IDeleteOrderService delete)
        {
            _create = create;
            _update = update;
            _get = get;
            _list = list;
            _delete = delete;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] OrderModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Body is required.");

            if (model.Lines == null || !model.Lines.Any())
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Lines are required.");

            try
            {
                var order = _create.Create(model.CustomerId, model.Lines.Select(l => (l.ProductId, l.Quantity)));
                return Found(new OrderData(order));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage Get(Guid orderId)
        {
            var order = _get.Get(orderId);
            if (order == null) return DoesNotExist();
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage List(int skip = 0, int take = 50, OrderStatus? status = null, Guid? customerId = null)
        {
            var orders = _list.Get(status, customerId)
                .Skip(skip)
                .Take(take)
                .Select(o => new OrderData(o))
                .ToList();

            return Found(orders);
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage Update(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Body is required.");

            var order = _get.Get(orderId);
            if (order == null) return DoesNotExist();

            try
            {
                var linesTuple = model.Lines == null ? null : model.Lines.Select(l => (l.ProductId, l.Quantity));
                _update.Update(order, model.CustomerId, model.Status, linesTuple);
                return Found(new OrderData(order));
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{orderId:guid}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid orderId)
        {
            _delete.Delete(orderId);
            return Found();
        }
    }
}
