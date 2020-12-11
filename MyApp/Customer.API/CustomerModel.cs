using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.API
{
    /// <summary>
    /// Customer Entity
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Uniqie Customer Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Customer's first name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Customer's date of birth format: yyyy-MM-dd
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
