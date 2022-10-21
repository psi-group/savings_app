using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupsController : ControllerBase
    {
        private readonly savingsAppContext _context;

        public PickupsController(savingsAppContext context)
        {
            _context = context;
        }

        // GET: api/Pickups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pickup>>> GetPickup()
        {
            return await _context.Pickup.ToListAsync();
        }

        



        // GET: api/Pickups/5
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<Pickup>>> GetProductPickup(Guid productId)
        {
            return await _context.Pickup
                .Where((pickup) => pickup.productId == productId)
                .ToListAsync();
        }

        // PUT: api/Pickups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPickup(Guid id, Pickup pickup)
        {
            if (id != pickup.Id)
            {
                return BadRequest();
            }

            _context.Entry(pickup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupExists(id))
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

        // POST: api/Pickups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pickup>> PostPickup(Pickup pickup)
        {
            _context.Pickup.Add(pickup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPickup", new { id = pickup.Id }, pickup);
        }

        // DELETE: api/Pickups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePickup(Guid id)
        {
            var pickup = await _context.Pickup.FindAsync(id);
            if (pickup == null)
            {
                return NotFound();
            }

            _context.Pickup.Remove(pickup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PickupExists(Guid id)
        {
            return _context.Pickup.Any(e => e.Id == id);
        }
    }
}
