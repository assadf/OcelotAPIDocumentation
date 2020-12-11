using Microsoft.AspNetCore.Mvc;
using System;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Return a customer
        /// </summary>
        /// <remarks>
        /// 
        /// GET /customers
        /// 
        /// This will return a customer
        /// 
        /// </remarks>
        /// <returns></returns>
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