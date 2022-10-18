using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;
using System.Xml.Linq;

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

        [HttpGet]
        public IEnumerable<Pickup> Get()
        {
            return _dataAccessService.GetPickups();
        }

        [HttpGet("byProductId/{id}")]
        public IEnumerable<Pickup> GetBy(Guid id)
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

        [HttpPut("{id}")]
        public void Update([FromBody] Pickup pickup)
        {
            _dataAccessService.UpdatePickup(pickup);
        }
    }
}
