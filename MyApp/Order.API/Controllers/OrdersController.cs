using Microsoft.AspNetCore.Mvc;
using System;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Return an order
        /// </summary>
        /// <remarks>
        /// 
        /// GET /orders
        /// 
        /// This will return an order
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(OrderModel), 200)]
        public IActionResult GetOrders()
        {
            var order = new OrderModel
            {
                Id = Guid.NewGuid(),
                Description = "This is an order"
            };

            return Ok(order);
        }
    }
}