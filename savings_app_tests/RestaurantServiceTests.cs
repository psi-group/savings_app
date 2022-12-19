using Application.Services.Implementations;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace savings_app_tests
{
    public class RestaurantServiceTests
    {

        private readonly RestaurantService _sut;
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();
        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<HttpContextAccessor>();
        private readonly IEmailSender _emailSender = Substitute.For<IEmailSender>();

        public RestaurantServiceTests()
        {
            _sut = new RestaurantService(_httpContext, _restaurantRepository, _fileSaver);
        }

        [Fact]
        public async Task GetRestaurant_ShouldReturnRestaurant_WhenRestaurantExists()
        {
            //Arrange

            var id = Guid.NewGuid();
            var userAuthid = Guid.NewGuid();
            var addressId = Guid.NewGuid();

            var returnedRestaurant = new Restaurant(
                id,
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
                    1,
                    1
                    ),
                null,
                true,
                "description",
                "shortDesc",
                "ref");

            _restaurantRepository.GetRestaurantAsync(id).Returns(returnedRestaurant);

            //Act

            var restaurantDTO = await _sut.GetRestaurant(id);

            //Assert

            Assert.Equal(returnedRestaurant.Name, restaurantDTO.Name);
            Assert.Equal(returnedRestaurant.SiteRef, restaurantDTO.SiteRef);
            Assert.Equal(returnedRestaurant.ImageUrl, restaurantDTO.ImageUrl);
            Assert.Equal(returnedRestaurant.Open, restaurantDTO.Open);
            Assert.Equal(returnedRestaurant.Description, restaurantDTO.Description);
            Assert.Equal(returnedRestaurant.ShortDescription, restaurantDTO.ShortDescription);

            Assert.Equal(returnedRestaurant.Address.Country, restaurantDTO.Address.Country);
            Assert.Equal(returnedRestaurant.Address.City, restaurantDTO.Address.City);
            Assert.Equal(returnedRestaurant.Address.StreetName, restaurantDTO.Address.StreetName);
            Assert.Equal(returnedRestaurant.Address.HouseNumber, restaurantDTO.Address.HouseNumber);
            Assert.Equal(returnedRestaurant.Address.AppartmentNumber, restaurantDTO.Address.AppartmentNumber);
            Assert.Equal(returnedRestaurant.Address.PostalCode, restaurantDTO.Address.PostalCode);

        }

        [Fact]
        public async Task GetRestaurantPrivate_ShouldThrowRecourseNotFoundException_WhenRestaurantDoesNotExistAndIdentityMatchesRestaurantId()
        {
            //Arrange

            var id = Guid.NewGuid();

            _restaurantRepository.GetRestaurantAsync(id).Returns(default(Restaurant));

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act



            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetRestaurant(id));
        }


        [Fact]
        public async Task GetRestaurantPrivate_ShouldReturnRestaurant_WhenRestaurantExistsAndIdentityMatchesRestaurantId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var userAuthid = Guid.NewGuid();
            var addressId = Guid.NewGuid();

            var returnedRestaurant = new Restaurant(
                id,
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
                    1,
                    1
                    ),
                null,
                true,
                "description",
                "shortDesc",
                "ref");

            _restaurantRepository.GetRestaurantAsync(id).Returns(returnedRestaurant);


            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var restaurantDTO = await _sut.GetRestaurantPrivate(id);

            //Assert

            Assert.Equal(returnedRestaurant.Name, restaurantDTO.Name);
            Assert.Equal(returnedRestaurant.SiteRef, restaurantDTO.SiteRef);
            Assert.Equal(returnedRestaurant.ImageUrl, restaurantDTO.ImageUrl);
            Assert.Equal(returnedRestaurant.Open, restaurantDTO.Open);
            Assert.Equal(returnedRestaurant.Description, restaurantDTO.Description);
            Assert.Equal(returnedRestaurant.ShortDescription, restaurantDTO.ShortDescription);

            Assert.Equal(returnedRestaurant.Address.Country, restaurantDTO.Address.Country);
            Assert.Equal(returnedRestaurant.Address.City, restaurantDTO.Address.City);
            Assert.Equal(returnedRestaurant.Address.StreetName, restaurantDTO.Address.StreetName);
            Assert.Equal(returnedRestaurant.Address.HouseNumber, restaurantDTO.Address.HouseNumber);
            Assert.Equal(returnedRestaurant.Address.AppartmentNumber, restaurantDTO.Address.AppartmentNumber);
            Assert.Equal(returnedRestaurant.Address.PostalCode, restaurantDTO.Address.PostalCode);

        }

        [Fact]
        public async Task GetRestaurantPrivate_ShouldThrowRecourseNotFoundException_WhenIdentityMatchesRestaurantIdButRestaurantDoesntExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _restaurantRepository.GetRestaurantAsync(id).Returns((Restaurant?)null);

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetRestaurantPrivate(id));

        }

        [Fact]
        public async Task GetRestaurant_ShouldThrowRecourseNotFoundException_WhenRestaurantDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _restaurantRepository.GetRestaurantAsync(id).Returns(default(Restaurant));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetRestaurant(id));
        }


        [Fact]
        public async Task PostRestaurant_ShouldReturnRestaurantDTOResponseAndSaveToDatabaseAndSaveImage_WhenRestaurantWithGivenEmailDoesntExistAndImageIsNotNull()
        {
            //Arrange

            var restaurantToPost = new RestaurantDTORequest(
                "restaurant",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                new AddressDTORequest(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                Substitute.For<IFormFile>(),
                true,
                null,
                null,
                null
                );

            var restaurantDTOResponse = new RestaurantDTOResponse(
                Guid.NewGuid(),
                "restaurant",
                new AddressDTOResponse(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                null,
                true,
                null,
                null,
                null
                );


            int saveImageCounter = 0;
            int saveChangesAsyncCounter = 0;
            int addBuyerAsyncCounter = 0;

            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<String>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);
            _restaurantRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _restaurantRepository.When(async x => await x.AddRestaurantAsync(Arg.Any<Restaurant>()))
                .Do(x => addBuyerAsyncCounter++);

            _restaurantRepository.GetRestaurantAsync(Arg.Any<Expression<Func<Restaurant, bool>>>()).Returns((Restaurant?)null);

            //Act

            var returnedRestaurantDTOResponse = await _sut.PostRestaurant(restaurantToPost);

            //Assert

            Assert.Equal(restaurantDTOResponse.Name, returnedRestaurantDTOResponse.Name);
            Assert.Equal(restaurantDTOResponse.SiteRef, returnedRestaurantDTOResponse.SiteRef);
            Assert.Equal(restaurantDTOResponse.Open, returnedRestaurantDTOResponse.Open);

            Assert.Equal(restaurantDTOResponse.Address.Country, returnedRestaurantDTOResponse.Address.Country);
            Assert.Equal(restaurantDTOResponse.Address.City, returnedRestaurantDTOResponse.Address.City);
            Assert.Equal(restaurantDTOResponse.Address.StreetName, returnedRestaurantDTOResponse.Address.StreetName);
            Assert.Equal(restaurantDTOResponse.Address.HouseNumber, returnedRestaurantDTOResponse.Address.HouseNumber);
            Assert.Equal(restaurantDTOResponse.Address.AppartmentNumber, returnedRestaurantDTOResponse.Address.AppartmentNumber);
            Assert.Equal(restaurantDTOResponse.Address.PostalCode, returnedRestaurantDTOResponse.Address.PostalCode);



            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, addBuyerAsyncCounter);
        }

        [Fact]
        public async Task PostRestaurant_ShouldThrowRecourseAlreadyExistsException_WhenRestaurantWithGivenEmailExists()
        {
            //Arrange

            var restaurantToPost = new RestaurantDTORequest(
                "restaurant",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                new AddressDTORequest(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                Substitute.For<IFormFile>(),
                true,
                null,
                null,
                null
                );

            var restaurant = new Restaurant(
                Guid.NewGuid(),
                "restaurant",
                new UserAuth(
                    "email",
                    "password"
                    ),
                new Address(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                null,
                true,
                null,
                null,
                null
                );

            _restaurantRepository.GetRestaurantAsync(Arg.Any<Expression<Func<Restaurant, bool>>>()).Returns(restaurant);

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseAlreadyExistsException>(async () => await _sut.PostRestaurant(restaurantToPost));
        }

    }
}