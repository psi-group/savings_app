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

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
            return Ok(await _orderService.GetOrder(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> PutOrder(Guid id, OrderDTORequest order)
        {
            return Ok(await _orderService.PutOrder(id, order));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<OrderDTOResponse>> DeleteOrder(Guid id)
        {
            return Ok(await _orderService.DeleteOrder(id));
        }
    }
}
