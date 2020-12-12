using System;
using System.ComponentModel.DataAnnotations;

namespace OrderProviderB.API
{
    public class CreateOrderCommand
    {
        [SwaggerIgnoreProperty]
        public Guid OrderId { get; set; }

        [SwaggerIgnoreProperty]
        public string OrderName { get; set; }

        /// <summary>
        /// Order Provider B Data
        /// </summary>
        [Required]
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

        /// <summary>
        /// Customer's current passport number
        /// </summary>
        [Required]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Customer's Date Of Birth format: yyyy-MM-dd
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
