using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupsController : ControllerBase
    {
        private readonly IPickupService _pickupService;

        public PickupsController(IPickupService pickupService)
        {
            _pickupService = pickupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PickupDTOResponse>> GetPickup(Guid id)
        {
            return Ok(await _pickupService.GetPickup(id));
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<PickupDTOResponse>>> GetProductPickups(Guid productId)
        {
            return Ok(await _pickupService.GetProductPickups(productId));
        }

        [Authorize(Roles = "seller")]
        [HttpPost]
        public async Task<ActionResult<PickupDTOResponse>> PostPickup(PickupDTORequest pickup)
        {
            return Ok(await _pickupService.PostPickup(pickup));
        }
    }
}
