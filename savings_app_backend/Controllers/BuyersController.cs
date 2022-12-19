using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IBuyerService _buyerService;

        public BuyersController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyerDTOResponse>>> GetBuyers()
        {
            return Ok(await _buyerService.GetBuyers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuyerDTOResponse>> GetBuyer(Guid id)
        {
            return Ok(await _buyerService.GetBuyer(id));
        }

        [Authorize(Roles = "buyer")]
        [HttpGet("private/{id}")]
        public async Task<ActionResult<BuyerPrivateDTOResponse>> GetBuyerPrivate(Guid id)
        {
            return Ok(await _buyerService.GetBuyerPrivate(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<BuyerDTOResponse>> PutBuyer(Guid id, BuyerDTORequest Buyer)
        {
            return Ok(await _buyerService.PutBuyer(id, Buyer));
        }

        [HttpPost]
        public async Task<ActionResult<BuyerDTOResponse>> PostBuyer(
            [FromForm] BuyerDTORequest Buyer)
        {
            return Ok(await _buyerService.PostBuyer(Buyer));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "buyer")]
        public async Task<ActionResult<BuyerDTOResponse>> DeleteBuyer(Guid id)
        {
            return Ok(await _buyerService.DeleteBuyer(id));
        }
    }
}
