using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataAccessServiceProducts _dataAccessService;

        public ProductsController(DataAccessServiceProducts dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dataAccessService.GetProducts();
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return _dataAccessService.GetById(id);
        }

        [HttpGet]
        [Route("filter")]
        public IEnumerable<Product> Get([FromQuery] string[] filter, string? search)
        {
            return _dataAccessService.GetWithFilters(filter, search);
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _dataAccessService.AddProduct(product);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _dataAccessService.Delete(id);
        }

    }
}
