using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Implementations;
using Domain.Interfaces.Repositories;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Enums;
using Domain.DTOs.Request;
using Xunit;

namespace savings_app_tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();

        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<HttpContextAccessor>();
        private readonly IEmailSender _emailSender = Substitute.For<IEmailSender>();

        

        public ProductServiceTests()
        {
            _sut = new ProductService(_httpContext, _productRepository, _fileSaver);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var returnedProduct = new Product(id, "product", Category.Snack, Guid.NewGuid(), null, AmountType.Unit,
                1.0f, 2, 1.0f, null, DateTime.Now, "description");

            _productRepository.GetProductAsync(id).Returns(returnedProduct);

            //Act

            var productDTOResponse= await _sut.GetProduct(id);

            //Assert

            Assert.Equal(returnedProduct.Name, productDTOResponse.Name);
            Assert.Equal(returnedProduct.Id, productDTOResponse.Id);
            Assert.Equal(returnedProduct.Category, productDTOResponse.Category);
            Assert.Equal(returnedProduct.RestaurantID, productDTOResponse.RestaurantID);
            Assert.Equal(returnedProduct.AmountOfUnits, productDTOResponse.AmountOfUnits);
            Assert.Equal(returnedProduct.AmountPerUnit, productDTOResponse.AmountPerUnit);
            Assert.Equal(returnedProduct.AmountType, productDTOResponse.AmountType);
            Assert.Equal(returnedProduct.ImageUrl, productDTOResponse.ImageUrl);
            Assert.Equal(returnedProduct.ShelfLife, productDTOResponse.ShelfLife);
            Assert.Equal(returnedProduct.Description, productDTOResponse.Description);


        }

        [Fact]
        public async Task GetProduct_ShouldThrowRecourseNotFoundException_WhenProductDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _productRepository.GetProductAsync(id).Returns(default(Product));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetProduct(id));
        }
        /*
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
        */
        [Fact]
        public async Task PostProduct_ShouldReturnProductAndSaveImageAndAddToDatabase_WhenIdentityMatchesRestaurantsIdAndImageIsNotNull()
        {
            //Arrange

            var id = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();

            var productToPost = new ProductDTORequest("product", false, Category.Snack, restaurantId, AmountType.Unit,
                1.0f, 2, 1.0f, DateTime.Now, "description", Substitute.For<IFormFile>());

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", restaurantId.ToString()) });


            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            int saveImageCounter = 0;
            int addProductCounter = 0;
            int saveChangesCounter = 0;


            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<string>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);
            _productRepository.When(x => x.AddProductAsync(Arg.Any<Product>()))
                .Do(x => addProductCounter++);
            _productRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesCounter++);

            //Act

            var productDTOResponse = await _sut.PostProduct(productToPost);

            //Assert

            Assert.Equal(productToPost.Name, productDTOResponse.Name);
            Assert.Equal(productDTOResponse.Description, productDTOResponse.Description);
            Assert.Equal(productToPost.IsHidden, productDTOResponse.IsHidden);
            Assert.Equal(productToPost.Category, productDTOResponse.Category);
            Assert.Equal(productToPost.RestaurantID, productDTOResponse.RestaurantID);
            Assert.Equal(productToPost.AmountType, productDTOResponse.AmountType);
            Assert.Equal(productToPost.AmountOfUnits, productDTOResponse.AmountOfUnits);
            Assert.Equal(productToPost.AmountPerUnit, productDTOResponse.AmountPerUnit);
            Assert.Equal(productToPost.Price, productDTOResponse.Price);
            Assert.Equal(productToPost.ShelfLife, productDTOResponse.ShelfLife);

            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, addProductCounter);
            Assert.Equal(1, saveChangesCounter);
        }
        /*
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
        }*/
    }
}