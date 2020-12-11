using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        /// <example>
        /// <code>
        /// var client = new HttpClient();
        /// var response = await client.GetAsync("/orders");
        /// </code>
        /// </example>
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
        /// 
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
}