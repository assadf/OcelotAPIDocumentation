using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        [ProducesResponseType(404)]
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
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[ApiExplorerSettings(IgnoreApi = true)] // Use this to ignore an endpoint
        public async Task<IActionResult> PostOrdersAsyc([FromBody]CreateOrderCommand command)
        {
            var client = new HttpClient();

            if (command.Name.ToLowerInvariant() == "orderprovidera")
            {
                var model = new
                {
                    OrderId = Guid.NewGuid(),
                    OrderName = "Order Provider A Placed",
                    Data = JObject.Parse(command.Data.ToString())
                };

                var message = JsonConvert.SerializeObject(model);
                var content = new StringContent(message, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://orderprovidera-api/api/orders", content);

                response.EnsureSuccessStatusCode();
            }

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