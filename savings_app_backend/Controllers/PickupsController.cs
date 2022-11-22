using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Services.Interfaces;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupsController : ControllerBase
    {
        private readonly IPickupService _pickupService;
        private readonly ILogger<PickupsController> _logger;

        public PickupsController(IPickupService pickupService,
            ILogger<PickupsController> logger)
        {
            _pickupService = pickupService;
            _logger = logger;
        }

        // GET: api/Pickups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pickup>>> GetPickups()
        {
            return Ok(await _pickupService.GetPickups());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pickup>> GetPickup(Guid id)
        {
            try
            {
                return Ok(await _pickupService.GetPickups());
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // GET: api/Pickups/5
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<Pickup>>> GetProductPickups(Guid productId)
        {
            return Ok(await _pickupService.GetProductPickups(productId));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pickup>> PutPickup(Guid id, Pickup pickup)
        {
            try
            {
                return Ok(await _pickupService.PutPickup(id, pickup));
            }
            catch(InvalidRequestArgumentsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
            catch(RecourseAlreadyExistsException e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
        }

        // POST: api/Pickups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pickup>> PostPickup(Pickup pickup)
        {
            return Ok(await _pickupService.PostPickup(pickup));
        }

        // DELETE: api/Pickups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pickup>> DeletePickup(Guid id)
        {
            try
            {
                return Ok(await _pickupService.DeletePickup(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }
    }
}
