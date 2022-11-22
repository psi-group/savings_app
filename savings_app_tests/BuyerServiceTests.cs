using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Implementations;
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
    public class BuyerServiceTests
    {
        private readonly BuyerService _sut;

        private readonly IBuyerRepository _buyerRepository = Substitute.For<IBuyerRepository>();

        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<HttpContextAccessor>();

        public BuyerServiceTests()
        {
            _sut = new BuyerService(_httpContext, _webHostEnvironment, _configuration, _buyerRepository, _fileSaver);
        }

        [Fact]
        public async Task GetBuyer_ShouldReturnBuyer_WhenBuyerExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var returnedBuyer = new Buyer(id, "buyer", Guid.NewGuid(), Guid.NewGuid(), id.ToString());

            _buyerRepository.GetBuyer(id).Returns(returnedBuyer);

            //Act

            var buyer = await _sut.GetBuyer(id);

            //Assert

            Assert.Equal(returnedBuyer, buyer);
        }

        [Fact]
        public async Task GetBuyer_ShouldThrowRecourseNotFoundException_WhenBuyerDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _buyerRepository.GetBuyer(id).Returns(default(Buyer));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetBuyer(id));
        }
    }
}
