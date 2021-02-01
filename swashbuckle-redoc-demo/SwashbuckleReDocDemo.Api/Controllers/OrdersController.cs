using System;
using System.Collections.Generic;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwashbuckleReDocDemo.Api.Models;
using SwashbuckleReDocDemo.Api.Requests;

namespace SwashbuckleReDocDemo.Api.Controllers
{
    /// <summary>
    /// Manage all the orders in the system
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly Fixture _fixture = new();

        /// <summary>
        /// Returns all the orders
        /// </summary>
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _fixture.CreateMany<Order>(20);
        }
        
        /// <summary>
        /// Returns an order by id
        /// </summary>
        /// <param name="id">Id of the order</param>
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public Order Get(Guid id)
        {
            return _fixture.Create<Order>();
        }
        
        /// <summary>
        /// Adds an order
        /// </summary>
        /// <response code="201">Created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderRequest request)
        {
            return Created("/orders/5880FA4C-F2D1-45B3-94FD-DFF7671960BA", _fixture.Create<Order>());
        }
        
        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="id">Id of the order</param>
        /// <response code="204">Deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return NoContent();
        }
    }
}