using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupsController : ControllerBase
    {
        private DataAccessServicePickups _dataAccessService;

        public PickupsController(DataAccessServicePickups dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Pickup> Get()
        {
            return _dataAccessService.GetPickups();
        }

        [HttpGet("byProductId/{id}")]
        public IEnumerable<Pickup> GetBy(string id)
        {
            var pickups = _dataAccessService.GetPickups();
            List<Pickup> pickupsByProductId = new List<Pickup>();
            foreach (var pickup in pickups)
            {
                if(pickup.productId == id)
                {
                    pickupsByProductId.Add(pickup);
                }
            }

            return pickupsByProductId;
        }

        // GET api/<ProductsController>/5
        /*[HttpGet("{id}")]
        public Restaurant Get(int id)
        {
            return _dataAccessService.GetById(id);
        }*/



    }
}
