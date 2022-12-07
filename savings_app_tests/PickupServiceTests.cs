/*using Microsoft.AspNetCore.Http;
using NSubstitute;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Implementations;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            _pickupRepository.GetPickup(id).Returns(returnedPickup);

            //Act

            var pickup = await _sut.GetPickup(id);

            //Assert

            Assert.Equal(returnedPickup, pickup);
        }

        [Fact]
        public async Task GetPickup_ShouldThrowRecourseNotFoundException_WhenPickupDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _pickupRepository.GetPickup(id).Returns(default(Pickup));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetPickup(id));
        }

        

    }
}
*/