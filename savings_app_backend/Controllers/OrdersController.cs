using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models;
using savings_app_backend.WebSite.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private DataAccessServiceOrders _dataAccessService;

        public OrdersController(DataAccessServiceOrders dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _dataAccessService.GetOrders();
        }

        [HttpGet]
        [Route("byBuyerId/{buyerId}")]
        public IEnumerable<Order> GetByBuyerId(Guid buyerId)
        {
            return _dataAccessService.GetByBuyerId(buyerId);
        }

        [HttpPost]
        public void Create([FromBody] Order order)
        {
            _dataAccessService.CreateOrder(order);
        }

        
    }
}
