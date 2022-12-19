using Application.Services.Implementations;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using Xunit;

namespace savings_app_tests
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _config = Substitute.For<IConfiguration>();
        private readonly IBuyerRepository _buyerRepo = Substitute.For<IBuyerRepository>();
        private readonly IRestaurantRepository _restaurantRepo = Substitute.For<IRestaurantRepository>();


        private readonly AuthService _sut;

        public AuthServiceTests()
        {
            _sut = new AuthService(_config, _buyerRepo, _restaurantRepo);
        }

        [Fact]
        public void GenerateToken_ShouldGenerateTokenForBuyer()
        {

            // Arrange

            User user = new Buyer(
                Guid.NewGuid(),
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                "imageUrl"
                );

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

        [Fact]
        public async Task AuthenticateUser_ShouldReturnBuyer_WhenThereIsBuyerWithLoginAndPasswordMatches()
        {

            // Arrange

            Buyer buyer = new Buyer(
                Guid.NewGuid(),
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                "imageUrl"
                );

            _buyerRepo.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns(buyer);

            var userLogin = new UserLoginDTO(
                "email",
                "password"
                );

            // Act

            var buyerResponse = await _sut.AuthenticateUserAsync(userLogin);

            // Assert


            Assert.Equal(buyer, buyerResponse);

        }

        [Fact]
        public async Task AuthenticateUser_ShouldThrowInvalidLoginCredentialsException_WhenThereIsBuyerWithLoginButPasswordDoesntMatch()
        {

            // Arrange

            Buyer buyer = new Buyer(
                Guid.NewGuid(),
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                "imageUrl"
                );

            _buyerRepo.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns(buyer);

            var userLogin = new UserLoginDTO(
                "email",
                "wrongPassword"
                );

            // Act
            // Assert


            await Assert.ThrowsAsync<InvalidLoginCredentialsException>(async () => await _sut.AuthenticateUserAsync(userLogin));

        }


        [Fact]
        public async Task AuthenticateUser_ShouldReturnSeller_WhenThereIsNoBuyerWithLoginAndThereIsSellerWithLoginAndPasswordMatches()
        {

            // Arrange

            _buyerRepo.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);

            var seller = new Restaurant(
                Guid.NewGuid(),
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                null,
                true,
                null,
                null,
                null
                );

            _restaurantRepo.GetRestaurantAsync(Arg.Any<Expression<Func<Restaurant, bool>>>()).Returns(seller);

            var userLogin = new UserLoginDTO(
                "email",
                "password"
                );

            // Act

            var restaurant = await _sut.AuthenticateUserAsync(userLogin);

            // Assert


            Assert.Equal(seller, restaurant);

        }


        [Fact]
        public async Task AuthenticateUser_ShouldThrowInvalidLoginCredentialsException_WhenThereIsNoBuyerWithLoginAndThereIsNoSellerWithLogin()
        {

            // Arrange

            _buyerRepo.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);


            _restaurantRepo.GetRestaurantAsync(Arg.Any<Expression<Func<Restaurant, bool>>>()).Returns((Restaurant?)null);

            var userLogin = new UserLoginDTO(
                "email",
                "password"
                );

            // Act


            // Assert


            await Assert.ThrowsAsync<InvalidLoginCredentialsException>(async () => await _sut.AuthenticateUserAsync(userLogin));

        }
    }
}
