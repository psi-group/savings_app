/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using savings_app_backend.EmailSender;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.SavingToFile;
using savings_app_backend.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class RestaurantServiceTests
    {

        private readonly RestaurantService _sut;
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();
        private readonly IUserAuthRepository _userAuthRepository = Substitute.For<IUserAuthRepository>();

        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<HttpContextAccessor>();
        private readonly IEmailSender _emailSender = Substitute.For<IEmailSender>();

        public RestaurantServiceTests()
        {
            _sut = new RestaurantService(_httpContext, _webHostEnvironment, _configuration, _restaurantRepository);
        }

        [Fact]
        public async Task GetRestaurant_ShouldReturnRestaurant_WhenRestaurantExists()
        {
            //Arrange

            var id = Guid.NewGuid();
            var userAuthid = Guid.NewGuid();
            var addressId = Guid.NewGuid();

            var returnedRestaurant = new Restaurant(id, "restaurant", userAuthid, addressId, id.ToString(),
                true, 5.0, "description", "shortDesc", "ref");

            _restaurantRepository.GetRestaurant(id).Returns(returnedRestaurant);

            //Act

            var restaurant = await _sut.GetRestaurant(id);

            //Assert

            Assert.Equal(restaurant, returnedRestaurant);
        }

        [Fact]
        public async Task GetRestaurant_ShouldThrowRecourseNotFoundException_WhenRestaurantDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _restaurantRepository.GetRestaurant(id).Returns(default(Restaurant));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetRestaurant(id));
        }

    }
}
*/