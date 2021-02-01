using System;
using System.ComponentModel.DataAnnotations;

namespace SwashbuckleReDocDemo.Api.Models
{
    public class Product
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}