using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using savings_app_backend.Exceptions;
using savings_app_backend.Extention;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Services.Interfaces;

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
            var user = AuthenticateUser(userLogin);

            if(user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("No such user found");
            }
        }

        private string GenerateToken(User user)
        {
            return _authService.GenerateToken(user);
        }

        private User AuthenticateUser(UserLoginDTO userLogin)
        {
            var buyer = _context.Buyers.FirstOrDefault(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                user.UserAuth.Password == userLogin.Password);

            if(buyer != null)
            {
                return buyer;
            }
            else
            {
                var seller = _context.Restaurants.FirstOrDefault(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                    user.UserAuth.Password == userLogin.Password);
                if(seller != null)
                {
                    return seller;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
