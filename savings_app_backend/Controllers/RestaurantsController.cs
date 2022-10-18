using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly DataAccessServiceRestaurants _dataAccessService;
        public RestaurantsController(DataAccessServiceRestaurants dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return _dataAccessService.GetRestaurants();
        }

        [HttpGet]
        [Route("filter")]
        public IEnumerable<Restaurant> Get([FromQuery] string? search)
        {
            return _dataAccessService.GetWithSearch(search);
        }


        [HttpGet("{id}")]
        public Restaurant GetByID(Guid id)
        {
            return _dataAccessService.GetById(id);
        }
    }
}
