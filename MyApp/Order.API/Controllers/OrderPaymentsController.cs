using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    /// <summary>
    /// Order Payments API - Get, Create etc
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderPaymentsController : ControllerBase
    {
        /// <summary>
        /// Return an order payment by Id
        /// </summary>
        /// <remarks>
        /// GET /orderpayments/{orderpaymenId}
        /// 
        /// This will return a single order payment for the given Id
        /// 
        /// </remarks>
        /// <returns>Collection of Orders.</returns>
        [HttpGet("{orderpaymentId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetOrderPayments(int orderPaymentId)
        {
            return Ok("This is an order payment");
        }
    }
}