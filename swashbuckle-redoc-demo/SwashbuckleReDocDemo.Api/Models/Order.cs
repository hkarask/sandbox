using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwashbuckleReDocDemo.Api.Models
{
    public class Order
    {
        /// <summary>
        /// Id of the order
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Name of the order
        /// </summary>
        /// <example>O-20200101</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Price of the order
        /// </summary>
        /// <example>10.99</example>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Whether the order is completed
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}