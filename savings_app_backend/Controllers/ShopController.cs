using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<string>> Checkout(CheckoutDTORequest checkout)
        {
            try
            {
                
                return Ok(await _shopService.Checkout(checkout));
            }
            catch(InvalidLoginCredentialsException)
            {
                return BadRequest("Invalid Login Credentials");
            }
        }
    }
}
