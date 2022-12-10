using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities.OrderAggregate;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTOResponse>>> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }

        [HttpGet("buyer/{buyerId}")]
        [Authorize (Roles = "buyer")]
        public async Task<ActionResult<IEnumerable<OrderDTOResponse>>> GetBuyersOrders(Guid buyerId)
        {
            return Ok(await _orderService.GetBuyersOrders(buyerId));
        }

        [HttpGet("orderItems/seller/{sellerId}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<IEnumerable<OrderItemDTOResponse>>>GetSellersOrderItems(Guid sellerId)
        {
            return Ok(await _orderService.GetSellersOrderItems(sellerId));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> GetOrder(Guid id)
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

        [HttpPut("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> PutOrder(Guid id, OrderDTORequest order)
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

        [HttpPost]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> PostOrder(OrderDTORequest order)
        {
            return Ok(await _orderService.PostOrder(order));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> DeleteOrder(Guid id)
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
