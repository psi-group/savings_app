using Application.Services.Interfaces;
using Castle.Core.Logging;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework.Internal;
using Org.BouncyCastle.Asn1.Mozilla;
using savings_app_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class ProductControllerTests
    {
        private readonly ProductsController _sut;

        private readonly IProductService _repository = Substitute.For<IProductService>();
        private readonly ILogger<ProductsController> _logger = Substitute.For<ILogger<ProductsController>>();

        public ProductControllerTests()
        {
            _sut = new ProductsController(_repository);
        }

        [Fact]
        public async Task testas()
        {
            // Arrange

            //ACt

            // assert
        }
    }
}
