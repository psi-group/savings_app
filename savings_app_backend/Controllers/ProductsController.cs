using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataAccessService _dataAccessService;

        public ProductsController(DataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dataAccessService.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _dataAccessService.GetById(id);
        }


        [HttpGet]
        [Route("filter")]
        public IEnumerable<Product> Get([FromQuery] string[] filter, string? search)
        {
            return _dataAccessService.GetWithFilters(filter, search);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _dataAccessService.AddProduct(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
