using Application.Services.Implementations;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;
using Xunit;

namespace savings_app_tests
{
    public class PickupServiceTests
    {
        PickupService _sut;

        private readonly IPickupRepository _pickupRepository = Substitute.For<IPickupRepository>();
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<IHttpContextAccessor>();

        public PickupServiceTests()
        {
            _sut = new PickupService(_pickupRepository, _productRepository, _restaurantRepository, _httpContext);
        }

        [Fact]
        public async Task GetPickup_ShouldReturnPickup_WhenPickupExists()
        {
            //Arrange

            var id = Guid.NewGuid();
            

            var returnedPickup = new Pickup(id, Guid.NewGuid(), DateTime.Now, DateTime.Now, PickupStatus.Available);
            var returnedPickupDTO = new PickupDTOResponse(
                id,
                returnedPickup.ProductId,
                returnedPickup.StartTime,
                returnedPickup.EndTime,
                returnedPickup.Status
                );

            _pickupRepository.GetPickupAsync(id).Returns(returnedPickup);

            //Act

            var pickupDTO = await _sut.GetPickup(id);

            //Assert

            Assert.Equal(returnedPickupDTO.Id, pickupDTO.Id);
            Assert.Equal(returnedPickupDTO.StartTime, pickupDTO.StartTime);
            Assert.Equal(returnedPickupDTO.Status, pickupDTO.Status);
            Assert.Equal(returnedPickupDTO.EndTime, pickupDTO.EndTime);
            Assert.Equal(returnedPickupDTO.ProductId, pickupDTO.ProductId);
        }

        [Fact]
        public async Task GetPickup_ShouldThrowRecourseNotFoundException_WhenPickupDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _pickupRepository.GetPickupAsync(id).Returns(default(Pickup));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetPickup(id));
        }

        [Fact]
        public async Task PostPickup_ShouldReturnPickupDTOResponseAndSaveToDatabase_WhenPostingPickupForProductThatExistsAndIdentityMatchesPickupProductsRestaurantId()
        {

            // Arrange
            var restaurantId = Guid.NewGuid();
            var product = new Product(
                Guid.NewGuid(),
                "product",
                0,
                restaurantId,
                null,
                0,
                0,
                0,
                0,
                null,
                DateTime.Now,
                null
                );

            _productRepository.GetProductAsync(product.Id).Returns(product);

            var restaurant = new Restaurant(
                restaurantId,
                "restaurant",
                new UserAuth(
                    "password",
                    "email"
                    ),
                new Address(
                    "country",
                    "city",
                    "street",
                    1,
                    null,
                    1),
                null,
                true,
                null,
                null,
                null
                    );
            _restaurantRepository.GetRestaurantAsync(restaurantId).Returns(restaurant);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", restaurantId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);


            int saveChangesAsyncCounter = 0;
            int addPickupAsyncCounter = 0;


            _pickupRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            
            _pickupRepository.When(async x => await x.AddPickupAsync(Arg.Any<Pickup>()))
                .Do(x => addPickupAsyncCounter++);

            var now = DateTime.Now;

            var pickupDTORequest = new PickupDTORequest(
                product.Id,
                now,
                now,
                0
                );

            // Act

            var pickupDTOResponse = await _sut.PostPickup(pickupDTORequest);

            // Assert

            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, addPickupAsyncCounter);

            Assert.Equal(pickupDTORequest.ProductId, pickupDTOResponse.ProductId);
            Assert.Equal(pickupDTORequest.StartTime, pickupDTOResponse.StartTime);
            Assert.Equal(pickupDTORequest.EndTime, pickupDTOResponse.EndTime);
            Assert.Equal(pickupDTORequest.Status, pickupDTOResponse.Status);
        }
    }
}
