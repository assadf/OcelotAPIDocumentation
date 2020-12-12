using Microsoft.AspNetCore.Mvc;

namespace OrderProviderB.API.Controllers
{
    /// <summary>
    /// Order Provider B API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Order Provider B Data Model
        /// </summary>
        /// <remarks>
        /// Order Provider B Data Model to be submitted
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateOrderProviderA")]
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