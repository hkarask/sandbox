using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwashbuckleReDocDemo.Api.Models;
using SwashbuckleReDocDemo.Api.Requests;

namespace SwashbuckleReDocDemo.Api.Controllers
{
    /// <summary>
    /// Manage all the products in the system
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Returns all the products
        /// </summary>
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return new List<Product>();
        }
        
        /// <summary>
        /// Returns a product by id
        /// </summary>
        /// <param name="id">Id of the product</param>
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IEnumerable<Product> Get(Guid id)
        {
            return new List<Product>();
        }
        
        /// <summary>
        /// Adds a prodcut
        /// </summary>
        /// <param name="request">Request</param>
        /// <response code="201">Created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post([FromBody] CreateProductRequest request)
        {
            return Created("/products/5880FA4C-F2D1-45B3-94FD-DFF7671960BA", new Product());
        }
        
        /// <summary>
        /// Deletes an product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <response code="204">Deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public IActionResult Delete(Guid productId)
        {
            return Ok();
        }
    }
}