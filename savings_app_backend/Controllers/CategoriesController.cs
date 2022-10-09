using Microsoft.AspNetCore.Mvc;
using savings_app.Models;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private DataAccessServiceCategories _dataAccessService;

        public CategoriesController(DataAccessServiceCategories dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _dataAccessService.GetCategories();
        }

        
    }
}
