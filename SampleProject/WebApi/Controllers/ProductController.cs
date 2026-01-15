using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _create;
        private readonly IUpdateProductService _update;
        private readonly IGetProductService _get;
        private readonly IGetProductsService _list;
        private readonly IDeleteProductService _delete;

        public ProductController(
            ICreateProductService create,
            IUpdateProductService update,
            IGetProductService get,
            IGetProductsService list,
            IDeleteProductService delete)
        {
            _create = create;
            _update = update;
            _get = get;
            _list = list;
            _delete = delete;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] ProductModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Body is required.");

            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Sku) || model.Price == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Name, Sku, and Price are required.");

            var p = _create.Create(model.Name, model.Sku, model.Price.Value);
            return Found(new ProductData(p));
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage Get(Guid productId)
        {
            var p = _get.Get(productId);
            if (p == null) return DoesNotExist();
            return Found(new ProductData(p));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage List(string sku = null, string name = null, bool? isActive = null, int skip = 0, int take = 50)
        {
            var items = _list.Get(sku, name, isActive)
                .Skip(skip)
                .Take(take)
                .Select(p => new ProductData(p))
                .ToList();

            return Found(items);
        }

        [Route("{productId:guid}")]
        [HttpPut]
        public HttpResponseMessage Update(Guid productId, [FromBody] ProductModel model)
        {
            if (model == null)
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Body is required."
                );

            var product = _get.Get(productId);
            if (product == null)
                return DoesNotExist();

            _update.Update(
                product,
                model.Name,
                model.Sku,
                model.Price,
                model.IsActive
            );

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid productId)
        {
            _delete.Delete(productId);
            return Found();
        }
    }
}
