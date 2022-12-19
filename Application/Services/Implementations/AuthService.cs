using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public AuthService(IConfiguration config, IBuyerRepository buyerRepository, IRestaurantRepository restaurantRepository)
        {
            _config = config;
            _buyerRepository = buyerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<string> Login(UserLoginDTO userLogin)
        {
            var user = await AuthenticateUserAsync(userLogin);

            var token = GenerateToken(user);
            return token;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string role = "";
            if (user.GetType() == typeof(Buyer))
            {
                role = "buyer";
            }
            else
            {
                role = "seller";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> AuthenticateUserAsync(UserLoginDTO userLogin)
        {
            var buyer = await _buyerRepository.GetBuyerAsync(user => user.UserAuth.Email.ToLower() == userLogin.Email!.ToLower());


            if (buyer != null)
            {
                if (buyer.UserAuth.Password.ToLower() == userLogin.Password!.ToLower())
                    return buyer;
                else
                    throw new InvalidLoginCredentialsException("incorrect password");
            }
            else
            {
                var seller = await _restaurantRepository.GetRestaurantAsync(user => user.UserAuth.Email.ToLower() == userLogin.Email!.ToLower());
                if (seller != null)
                {
                    if (seller.UserAuth.Password.ToLower() == userLogin.Password!.ToLower())
                        return seller;
                    else
                        throw new InvalidLoginCredentialsException("incorrect password");
                }
                else
                {
                    throw new InvalidLoginCredentialsException("no user with this email exists");
                }
            }
        }
    }
}
