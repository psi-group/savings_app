using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(savingsAppContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/ProductContr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFilteredProducts(
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

            Expression <Func<Product, Object>> orderDelegate = order == "by_id" ? (product) => product.Id :
                order == "by_name" ? (product) => product.Name : (product) => product.Price;

            ActionResult<IEnumerable<Product>> products;

            products = await _context.Products
                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy(orderDelegate)
                .ToListAsync();

            if (String.IsNullOrEmpty(order) || order == "by_id")
            {
                products = await _context.Products
                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy((product) => product.Id)
                .ToListAsync();
            }
            else if(order == "by_name")
            {
                products = await _context.Products
                .Where(product => (String.IsNullOrEmpty(search) || product.Name.ToLower().Contains(search.ToLower())))
                .Where(product => (categories.Count() == 0 || categories.Contains(product.Category)))
                .OrderBy((product) => product.Name)
                .ToListAsync();
            }
            else if (order == "by_price")
            {
                products = await _context.Products
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
            var product = await _context.Products.FindAsync(id);
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
        //[Authorize(Roles = "buyer")]
        public async Task<ActionResult<Product>> PostProduct([FromForm] Product product)
        {
            var date = new DateTime().ToString();

            product.Id = Guid.NewGuid();

            if(product.Pickups != null)
            {
                foreach (var pickup in product.Pickups)
                {
                    pickup.Id = Guid.NewGuid();
                    pickup.ProductId = product.Id;
                    pickup.status = PickupStatus.Available;
                    _context.Pickups.Add(pickup);
                }
            }




            /*var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                product.RestaurantID = Guid.Parse(identity.FindFirst("Id").Value);

            }*/

            product.RestaurantID = Guid.Parse("54ab816e-1e45-4ece-ad31-7f839129c22c");

            var imageName = product.Id.ToString() + ".jpg";
            SaveImage(product.ImageFile, imageName);
            product.ImageName = imageName;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [NonAction]
        private async void SaveImage(IFormFile imageFile, string imageName)
        {
            var imagePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot",  "productImg", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
        }

        // DELETE: api/ProductContr/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
