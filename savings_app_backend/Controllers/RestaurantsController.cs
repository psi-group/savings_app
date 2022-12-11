﻿using Application.Services.Interfaces;
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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTOResponse>>> GetRestaurants()
        {
            return Ok(await _restaurantService.GetRestaurants());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<RestaurantDTOResponse>>> GetFilteredRestaurants([FromQuery] string? search)
        {
            return Ok(await _restaurantService.GetFilteredRestaurants(search));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTOResponse>> GetRestaurant(Guid id)
        {
            return Ok(await _restaurantService.GetRestaurant(id));
        }

        [Authorize(Roles = "seller")]
        [HttpGet("private/{id}")]
        public async Task<ActionResult<RestaurantPrivateDTOResponse>> GetRestaurantPrivate(Guid id)
        {
            return Ok(await _restaurantService.GetRestaurantPrivate(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<RestaurantDTOResponse>> PutRestaurant(Guid id, RestaurantDTORequest restaurant)
        {
            return Ok(await _restaurantService.PutRestaurant(id, restaurant));
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDTOResponse>> PostRestaurant(
            [FromForm] RestaurantDTORequest restaurant)
        {
            return Ok(await _restaurantService.PostRestaurant(restaurant));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<RestaurantDTOResponse>> DeleteRestaurant(Guid id)
        {
            return Ok(await _restaurantService.DeleteRestaurant(id));
        }
    }
}
