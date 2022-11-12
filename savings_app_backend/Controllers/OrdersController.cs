using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly savingsAppContext _context;

        public OrdersController(savingsAppContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders(Guid userId)
        {
            return await _context.Orders
                .Where((order) => order.BuyerId == userId)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            order.Id = Guid.NewGuid();
            order.Status = OrderStatus.Cancelled;
            var add = await  _context.Orders.AddAsync(order);
            var save = await _context.SaveChangesAsync();
            var hello = 5;
            // DOESNT WORK WHEN DOING CHECKOUT SECOND TIME
            // CRASHES AFTER THIS LINE

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
