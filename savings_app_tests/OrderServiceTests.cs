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
    public class OrderServiceTests
    {
        OrderService _sut;
        private readonly IOrderRepository _orderRepository = Substitute.For<IOrderRepository>();

        public OrderServiceTests()
        {
            _sut = new OrderService(_orderRepository);
        }

        [Fact]
        public async Task GetOrder_ShouldReturnOrder_WhenOrderExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var returnedOrder = new Order(id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                OrderStatus.AwaitingPickup, Guid.NewGuid());

            _orderRepository.GetOrder(id).Returns(returnedOrder);

            //Act

            var pickup = await _sut.GetOrder(id);

            //Assert

            Assert.Equal(returnedOrder, pickup);
        }

        [Fact]
        public async Task GetOrder_ShouldThrowRecourseNotFoundException_WhenOrderDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _orderRepository.GetOrder(id).Returns(default(Order));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetOrder(id));
        }
    }
}
*/