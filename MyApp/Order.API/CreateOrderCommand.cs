using System.ComponentModel.DataAnnotations;

namespace Order.API
{
    /// <summary>
    /// Create an order command
    /// </summary>
    public class CreateOrderCommand
    {
        /// <summary>
        /// Name of the order.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Number of items required.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
