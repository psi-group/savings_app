/*
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using savings_app_backend.EmailSender;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Services.Implementations;
using NUnit;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Exceptions;
using System.Net.Http;
using System.Security.Claims;
using savings_app_backend.SavingToFile;
using Microsoft.AspNetCore.Mvc;

namespace savings_app_tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();
        private readonly IUserAuthRepository _userAuthRepository = Substitute.For<IUserAuthRepository>();

        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<HttpContextAccessor>();
        private readonly IEmailSender _emailSender = Substitute.For<IEmailSender>();

        

        public ProductServiceTests()
        {
            _sut = new ProductService(_webHostEnvironment, _configuration, _httpContext, _emailSender,
                _productRepository, _userAuthRepository, _restaurantRepository, _fileSaver);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var returnedProduct = new Product(id, "product", Category.Snack, Guid.NewGuid(), AmountType.unit,
                1.0f, 2, 1.0f, id.ToString(), DateTime.Now, "description");

            _productRepository.GetProduct(id).Returns(returnedProduct);

            //Act

            var product = await _sut.GetProduct(id);

            //Assert

            Assert.Equal(product, returnedProduct);
        }

        [Fact]
        public async Task GetProduct_ShouldThrowRecourseNotFoundException_WhenProductDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _productRepository.GetProduct(id).Returns(default(Product));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetProduct(id));
        }

        [Fact]
        public async Task PostProduct_ShouldThrowInvalidIdentityException_WhenRestaurantCreatesProductWithInvalidRestaurantId()
        {
            //Arrange

            ControllerContext controllerBase = new ControllerContext();

            var id = Guid.NewGuid();
            var productToPost = new Product(id, "product", Category.Snack, Guid.NewGuid(), AmountType.unit,
                1.0f, 2, 1.0f, id.ToString(), DateTime.Now, "description");

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.PostProduct(productToPost));
        }

        [Fact]
        public async Task PostProduct_ShouldReturnProductAndSaveImage_WhenProductHasBeenCreated()
        {
            //Arrange

            int saveImageCounter = 0;
            int addProductCounter = 0;

            var id = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();
            var productToPost = new Product(id, "product", Category.Snack, restaurantId, AmountType.unit,
                1.0f, 2, 1.0f, id.ToString(), DateTime.Now, "description");

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", restaurantId.ToString()) });


            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);



            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => saveImageCounter++);
            _productRepository.When(x => x.AddProduct(productToPost))
                .Do(x => addProductCounter++);

            //Act

            var product = await _sut.PostProduct(productToPost);

            //Assert

            Assert.Equal(product, productToPost);
            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, addProductCounter);
        }

        [Fact]
        public async Task DeleteProduct_ShouldThrowRecourseNotFoundException_WhenProductDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _productRepository.GetProduct(id).Returns(default(Product));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.DeleteProduct(id));
        }

        [Fact]
        public async Task DeleteProduct_ShouldThrowInvalidIdentityException_WhenRestaurantTriesDeletingNotItsOwnProduct()
        {
            //Arrange

            var id = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();

            var productToDelete = new Product(id, "product", Category.Snack, restaurantId, AmountType.unit,
                1.0f, 2, 1.0f, id.ToString(), DateTime.Now, "description");


            HttpContext httpctx = Substitute.For<HttpContext>();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", Guid.NewGuid().ToString()) });

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _productRepository.GetProduct(id).Returns(productToDelete);

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.DeleteProduct(id));
        }

        [Fact]
        public async Task GetFilteredProducts_ShouldThrowInvalidRequestArgumentsException_WhenInvalidOrder()
        {
            //Arrange

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidRequestArgumentsException>(async () =>
                await _sut.GetFilteredProducts(default, default, "invalidOrder"));
        }

        public async Task GetFilteredProducts_ShouldThdrowInvalidRequestArgumentsException_WhenInvalidOrder()
        {
            //Arrange

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidRequestArgumentsException>(async () =>
                await _sut.GetFilteredProducts(default, default, "invalidOrder"));
        }
    }
}*/