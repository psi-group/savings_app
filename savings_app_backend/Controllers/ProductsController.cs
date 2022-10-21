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
    public class ProductsController : ControllerBase
    {
        private readonly savingsAppContext _context;

        public ProductsController(savingsAppContext context)
        {
            _context = context;
        }

        // GET: api/ProductContr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFilteredProduct(
            [FromQuery] List<string> category, [FromQuery] string? search, [FromQuery] string? order)
        {
            List<Category> categories = new List<Category>();
            foreach(string categ in category)
            {
                Category cat;
                try
                {
                    cat = (Category)Enum.Parse(typeof(Category), categ, true);
                }
                catch(Exception e)
                {
                    return BadRequest("Invalid category argument");
                }
                
                categories.Add(cat);
            }

            Func<Product, Object> orderDelegate = order == "by_id" ? (product) => product.Id :
                order == "by_name" ? (product) => product.Name : (product) => product.Price;

            ActionResult<IEnumerable<Product>> products;

            if(String.IsNullOrEmpty(order) || order == "by_id")
            {
                products = await _context.Product
                
                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy((product) => product.Id)
                .ToListAsync();
            }
            else if(order == "by_name")
            {
                products = await _context.Product

                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy((product) => product.Name)
                .ToListAsync();
            }
            else if (order == "by_price")
            {
                products = await _context.Product

                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy((product) => product.Price)
                .ToListAsync();
            }
            else
            {
                return BadRequest("Invalid order argument");
            }



            return products;
        }

        // GET: api/ProductContr/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            var str = product.Category.ToString();
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/ProductContr/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/ProductContr
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/ProductContr/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
