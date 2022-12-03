/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using NSubstitute;
using savings_app_backend.Controllers;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class ProductsControllerTests
    {
        ProductsController _sut;
        private readonly IProductService _productService = Substitute.For<IProductService>();
        private readonly ILogger<ProductsController> _logger = Substitute.For<ILogger<ProductsController>>();

        public ProductsControllerTests()
        {
            _sut = new ProductsController(_productService, _logger);
        }

        //[Fact]
        public async Task BuyProduct_ShouldReturnOkProductWithSmallerAmount_WhenEnoughOfProductExists()
        {

            // Arrange

            var id = Guid.NewGuid();

            var productToBuy = new Product(id, "product", Category.Snack, Guid.NewGuid(), AmountType.unit,
                1.0f, 2, 1.0f, id.ToString(), DateTime.Now, "description");

            var productAfterBuy = new Product(id, "product", Category.Snack, Guid.NewGuid(), AmountType.unit,
                1.0f, 1, 1.0f, id.ToString(), DateTime.Now, "description");

            _productService.Buy(id, 1).Returns(productAfterBuy);

            var controllerCtx = new ControllerContext();
            

            // Act

            

        }
        
    }
}
*/