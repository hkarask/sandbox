using System.ComponentModel.DataAnnotations;

namespace SwashbuckleReDocDemo.Api.Requests
{
    public class CreateProductRequest
    {
        /// <summary>
        /// Name of the order
        /// </summary>
        /// <example>Keyboard</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Price of the order in dollars
        /// <example>39.99</example>
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}