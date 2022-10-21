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
    public class AuthController : ControllerBase
    {
        private readonly savingsAppContext _context;

        public AuthController(savingsAppContext context)
        {
            _context = context;
        }

        // GET: api/Auth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAuth>>> GetUserAuth()
        {
            return await _context.UserAuth.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuth>> GetUserAuth(Guid id)
        {
            var userAuth = await _context.UserAuth.FindAsync(id);

            if (userAuth == null)
            {
                return NotFound();
            }

            return userAuth;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAuth(Guid id, UserAuth userAuth)
        {
            if (id != userAuth.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAuth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAuthExists(id))
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

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAuth>> PostUserAuth(UserAuth userAuth)
        {
            _context.UserAuth.Add(userAuth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAuth", new { id = userAuth.Id }, userAuth);
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAuth(Guid id)
        {
            var userAuth = await _context.UserAuth.FindAsync(id);
            if (userAuth == null)
            {
                return NotFound();
            }

            _context.UserAuth.Remove(userAuth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAuthExists(Guid id)
        {
            return _context.UserAuth.Any(e => e.Id == id);
        }
    }
}
