using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using IDocumentFilter = Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter;

namespace Order.API.Controllers
{
    /// <summary>
    /// Orders API - Get, Create etc
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Return a list of Orders
        /// </summary>
        /// <remarks>
        /// GET /orders
        /// 
        /// This will return a list of Orders.
        /// </remarks>
        /// <returns>Collection of Orders.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), 200)]
        public IActionResult GetOrders()
        {
            var order = new OrderModel
            {
                Id = Guid.NewGuid(),
                Description = "This is an order"
            };

            return Ok(order);
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <remarks>
        /// POST /orders
        /// 
        /// This will accept an order to be created
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(202)]
        //[ApiExplorerSettings(IgnoreApi = true)] // Use this to ignore an endpoint
        public IActionResult PostOrders([FromBody]CreateOrderCommand command)
        {
            return Accepted();
        }
    }

    //public class InjectSamples : IDocumentFilter
    //{

    //    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        var path = swaggerDoc.Paths.Where(x => x.Key.Contains("Values")).First().Value;

    //        path.Post.Parameters.FirstOrDefault().Extensions.Add("x-code-samples", "123456");

            
    //    }
    //}
}