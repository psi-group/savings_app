using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Services.Interfaces;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<RestaurantsController> _logger;

        public RestaurantsController(IRestaurantService restaurantService, 
            ILogger<RestaurantsController> logger)
        {
            _restaurantService = restaurantService;
            _logger = logger;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return Ok(await _restaurantService.GetRestaurants());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetFilteredRestaurants([FromQuery] string? search)
        {
            return Ok(await _restaurantService.GetFilteredRestaurants(search));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
        {
            try
            {
                return Ok(await _restaurantService.GetRestaurant(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurant)
        {
            try
            {
                return Ok(await _restaurantService.PutRestaurant(id, restaurant));
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
            catch (InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            return Ok(await _restaurantService.PostRestaurant(restaurant));
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(Guid id)
        {
            try
            {
                return Ok(await _restaurantService.DeleteRestaurant(id));
            }
            catch(RecourseNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound();
            }
            catch(InvalidIdentityException e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }
        }
    }
}
