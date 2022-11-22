using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using savings_app_backend.Models.Entities;
using savings_app_backend.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _config = Substitute.For<IConfiguration>();
        private readonly AuthService _sut;

        public AuthServiceTests()
        {
            _sut = new AuthService(_config);
        }

        [Fact]
        public void GenerateToken_ShouldGenerateTokenForBuyer()
        {

            // Arrange

            User user = new Buyer();

            user.Name = "user";
            user.Id = Guid.NewGuid();

            _config["Jwt:Key"].Returns("randomStringOfCharacters");
            _config["Jwt:Issuer"].Returns("https://localhost:7183");
            _config["Jwt:Audience"].Returns("https://localhost:7183");

            // Act

            var token = _sut.GenerateToken(user);

            // Assert

            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            var idClaim = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var nameClaim = tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var roleClaim = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            var issuerClaim = tokenS.Claims.First(claim => claim.Type == "iss").Value;
            var audienceClaim = tokenS.Claims.First(claim => claim.Type == "aud").Value;
            var expirationClaim = tokenS.Claims.First(claim => claim.Type == "exp").Value;

            Assert.Equal(user.Name, nameClaim);
            Assert.Equal(user.Id.ToString(), idClaim);
            Assert.Equal("buyer", roleClaim);
            Assert.Equal(_config["Jwt:Issuer"], issuerClaim);
            Assert.Equal(_config["Jwt:Audience"], audienceClaim);

        }
    }
}
