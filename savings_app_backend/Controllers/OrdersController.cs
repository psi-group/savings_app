using Application.Services.Interfaces;
using Domain.Entities.OrderAggregate;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;


        public OrdersController(IOrderService orderService,
            ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            try
            {
                return Ok(await _orderService.GetOrder(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            try
            {
                return Ok(await _orderService.PutOrder(id, order));
            }
            catch(InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            return Ok(await _orderService.PostOrder(order));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                return Ok(await _orderService.DeleteOrder(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }
    }
}
