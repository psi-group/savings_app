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

        public string Login(UserLoginDTO userLogin)
        {
            var user = AuthenticateUser(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            else
            {
                throw new InvalidLoginCredentialsException();
            }
            
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
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? AuthenticateUser(UserLoginDTO userLogin)
        {
            var buyer = _buyerRepository.GetBuyer(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                user.UserAuth.Password == userLogin.Password);

            if (buyer != null)
            {
                return buyer;
            }
            else
            {
                var seller = _restaurantRepository.GetRestaurant(user => user.UserAuth.Email.ToLower() == userLogin.Email.ToLower() &&
                    user.UserAuth.Password == userLogin.Password);
                if (seller != null)
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
