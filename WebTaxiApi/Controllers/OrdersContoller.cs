using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _ordersService;

        public OrdersController(IOrderService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult> GetOrder()
        {
            var orders =  await _ordersService.GetAllAsync();
            return Ok(orders);
        }

        // GET: /Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _ordersService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: /Order/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] Guid id, [FromBody] Order order)
        {
            order.Id = id;
            try
            {
                await _ordersService.UpdateAsync(order);
                return NoContent();
            }
            catch(AppException ex)
            {
                return BadRequest(new {messege = ex.Message});
            }
        }

        // POST: api/Order
        [HttpPost("add")]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _ordersService.CreateAsync(order);

            return Ok();
        }

        // DELETE: api/Order/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*var order = await _ordersService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }*/

            await _ordersService.DeleteAsync(id);

            return NoContent();
        }

        /*private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }*/
    }
}