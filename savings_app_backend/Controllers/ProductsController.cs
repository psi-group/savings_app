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
        public IEnumerable<Product> Get([FromQuery] string[] filter, string? search, string? order)
        {
            if(order == null)
            {
                return _dataAccessService.GetWithFilters(filters: filter, searchText: search);
            }
            else
                return _dataAccessService.GetWithFilters(filters: filter, searchText: search, order: order);

        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _dataAccessService.AddProduct(product);
        }

        [HttpPut("{id}")]
        public void Update([FromBody] Product product)
        {
            _dataAccessService.UpdateProduct(product);
        }

    }
}
