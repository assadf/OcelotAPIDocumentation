using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderProviderA.API
{
    public class OrderProviderA_CreateOrderCommand
    {
        //[JsonIgnore]
        public Guid OrderId { get; set; }

        //[JsonIgnore]
        public string OrderName { get; set; }

        /// <summary>
        /// Order Provider A Data
        /// </summary>
        public DataCommand Data { get; set; }
    }

    /// <summary>
    /// Order Provider A Data Object
    /// </summary>
    public class DataCommand
    {
        /// <summary>
        /// Customer's First Name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Customer's Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }
}
