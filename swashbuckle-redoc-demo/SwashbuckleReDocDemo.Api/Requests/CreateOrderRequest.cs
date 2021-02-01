using System.ComponentModel.DataAnnotations;

namespace SwashbuckleReDocDemo.Api.Requests
{
    public class CreateOrderRequest
    {
        /// <summary>
        /// Name of the order
        /// <example>Notebook</example>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Price of the order in dollars
        /// <example>19.99</example>
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}