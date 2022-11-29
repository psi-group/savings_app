
using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SavingsAppContext _context;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(SavingsAppContext context,
           IConfiguration config, IAuthService authService)
        {
            _authService = authService;
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDTO userLogin)
        {
            try
            {
                return Ok(_authService.Login(userLogin));
            }
            catch(InvalidLoginCredentialsException)
            {
                return BadRequest("Invalid Login Credentials");
            }
        }
    }
}
