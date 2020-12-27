using Microsoft.AspNetCore.Mvc;
using System;

namespace Customer.API.Controllers
{
    /// <summary>
    /// Customers API - Get, Create etc
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Return a customer
        /// </summary>
        /// <remarks>
        /// GET /customers
        /// 
        /// This will return a customer
        /// </remarks>
        /// <returns>A Customer</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerModel), 200)]
        public IActionResult Get()
        {
            var customer = new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith"
            };

            return Ok(customer);
        }
    }
}