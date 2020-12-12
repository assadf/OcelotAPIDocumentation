using Microsoft.AspNetCore.Mvc;

namespace OrderProviderA.API.Controllers
{
    /// <summary>
    /// Order Provider A API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Order Provider A Data Model
        /// </summary>
        /// <remarks>
        /// Order Provider A Data Model to be submitted
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name ="CreateOrderProviderA")]
        public IActionResult Post([FromBody]CreateOrderCommand command)
        {
            return Ok(new ResultModel
            {
                Id = command.OrderId,
                EventName = "OrderCreatedEvent"
            });
        }
    }
}