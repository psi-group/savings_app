using Application.Services.Implementations;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System.Security.Claims;
using Xunit;

namespace savings_app_tests
{
    public class OrderServiceTests
    {
        OrderService _sut;
        private readonly IOrderRepository _orderRepository = Substitute.For<IOrderRepository>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<IHttpContextAccessor>();

        public OrderServiceTests()
        {
            _sut = new OrderService(_orderRepository, _httpContext);
        }

        [Fact]
        public async Task GetBuyersOrders_ShouldReturnBuyersOrders_WhenIdentityMatchesOrdersBuyerId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                );

            _orderRepository.GetOrderAsync(id).Returns(returnedOrder);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", buyerId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var orderDTOResponse = await _sut.GetOrder(id);

            //Assert

            Assert.Equal(returnedOrder.Id, orderDTOResponse.Id);
            Assert.Equal(returnedOrder.BuyerId, orderDTOResponse.BuyerId);

            Assert.Equal(1, orderDTOResponse.OrderItems.Count);

            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Id, orderDTOResponse.OrderItems[0].Id);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderId, orderDTOResponse.OrderItems[0].OrderId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].ProductId, orderDTOResponse.OrderItems[0].ProductId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].PickupId, orderDTOResponse.OrderItems[0].PickupId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderItemStatus, orderDTOResponse.OrderItems[0].OrderItemStatus);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Price, orderDTOResponse.OrderItems[0].Price);

        }

        [Fact]
        public async Task GetOrder_ShouldReturnOrder_WhenOrderExists()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                );

            _orderRepository.GetOrderAsync(id).Returns(returnedOrder);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", buyerId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var orderDTOResponse = await _sut.GetOrder(id);

            //Assert

            Assert.Equal(returnedOrder.Id, orderDTOResponse.Id);
            Assert.Equal(returnedOrder.BuyerId, orderDTOResponse.BuyerId);

            Assert.Equal(1, orderDTOResponse.OrderItems.Count);

            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Id, orderDTOResponse.OrderItems[0].Id);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderId, orderDTOResponse.OrderItems[0].OrderId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].ProductId, orderDTOResponse.OrderItems[0].ProductId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].PickupId, orderDTOResponse.OrderItems[0].PickupId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderItemStatus, orderDTOResponse.OrderItems[0].OrderItemStatus);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Price, orderDTOResponse.OrderItems[0].Price);


        }


        [Fact]
        public async Task PutOrder_ShouldReturnOrderAndUpdateDatabase_WhenOrderExistsAndIndentityMatchesBuyersId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var orderItems = new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                };

            var returnedOrder = new Order(
                id,
                buyerId,
                orderItems
                );

            var orderDTO = new OrderDTORequest(
                buyerId,
                orderItems
                );

            _orderRepository.OrderExistsAsync(id).Returns(true);
            _orderRepository.GetOrderAsync(id).Returns(returnedOrder);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", buyerId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            int saveChangesAsyncCounter = 0;
            int updateOrderAsyncCounter = 0;

            _orderRepository.When(x => x.UpdateOrder(Arg.Any<Order>()))
            .Do(x => updateOrderAsyncCounter++);

            _orderRepository.When(async x => await x.SaveChangesAsync())
            .Do(x => saveChangesAsyncCounter++);

            //Act

            var orderDTOResponse = await _sut.PutOrder(id, orderDTO);

            //Assert

            Assert.Equal(returnedOrder.Id, orderDTOResponse.Id);
            Assert.Equal(returnedOrder.BuyerId, orderDTOResponse.BuyerId);

            Assert.Equal(1, orderDTOResponse.OrderItems.Count);

            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Id, orderDTOResponse.OrderItems[0].Id);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderId, orderDTOResponse.OrderItems[0].OrderId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].ProductId, orderDTOResponse.OrderItems[0].ProductId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].PickupId, orderDTOResponse.OrderItems[0].PickupId);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].OrderItemStatus, orderDTOResponse.OrderItems[0].OrderItemStatus);
            Assert.Equal(new List<OrderItem>(returnedOrder.OrderItems)[0].Price, orderDTOResponse.OrderItems[0].Price);

            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, updateOrderAsyncCounter);


        }

        [Fact]
        public async Task DeleteOrder_ShouldThrowRecourseNotFoundException_WhenOrderDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                );

            _orderRepository.GetOrderAsync(id).Returns((Order?)null);

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.DeleteOrder(id));
        }

        [Fact]
        public async Task DeleteOrder_ShouldThrowInvalidIdentityException_WhenOrderExistsButIdentityDoesNotMatchOrdersBuyersId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                );

            _orderRepository.GetOrderAsync(id).Returns(returnedOrder);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.DeleteOrder(id));
        }


        [Fact]
        public async Task GetOrder_ShouldThrowRecourseNotFoundException_WhenOrderDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                );

            _orderRepository.GetOrderAsync(id).Returns((Order?)null);

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetOrder(id));
        }

        

        [Fact]
        public async Task GetOrder_ShouldThrowInvalidIdentityException_WhenOrderExistsButIdentityDoesNotMatchOrdersBuyersId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrder = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                );

            _orderRepository.GetOrderAsync(id).Returns(returnedOrder);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            //Assert

            await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.GetOrder(id));
        }

        [Fact]
        public async Task GetBuyersOrders_ShouldReturnBuyersOrders_WhenIdentityMatchesBuyersId()
        {
            //Arrange

            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var returnedOrders = new List<Order>() {
                new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                )
            };

            _orderRepository.GetOrdersAsync(Arg.Any<ISpecification<Order>>()).Returns(returnedOrders);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", buyerId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var orderDTOResponse = (List<OrderDTOResponse>)await _sut.GetBuyersOrders(buyerId);

            //Assert

            Assert.Equal(returnedOrders[0].Id, orderDTOResponse[0].Id);
            Assert.Equal(returnedOrders[0].BuyerId, orderDTOResponse[0].BuyerId);

            Assert.Equal(1, orderDTOResponse[0].OrderItems.Count);

            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].Id, ((orderDTOResponse[0].OrderItems))[0].Id);
            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].OrderId, ((orderDTOResponse[0].OrderItems))[0].OrderId);
            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].ProductId, ((orderDTOResponse[0].OrderItems))[0].ProductId);
            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].PickupId, ((orderDTOResponse[0].OrderItems))[0].PickupId);
            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].OrderItemStatus, ((orderDTOResponse[0].OrderItems))[0].OrderItemStatus);
            Assert.Equal(new List<OrderItem>(returnedOrders[0].OrderItems)[0].Price, ((orderDTOResponse[0].OrderItems))[0].Price);

        }


        [Fact]
        public async Task DeleteOrder_ShouldReturnOrderDTOResponseAndDeleteOrderFromDatabase_WhenOrderExistsAndIdentityMatchesBuyersId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var buyerId = Guid.NewGuid();

            var order = new Order(
                id,
                buyerId,
                new List<OrderItem>()
                {
                    new OrderItem(
                        Guid.NewGuid(),
                        id,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1,
                        1,
                        OrderItemStatus.AwaitingPickup
                        )
                }
                );

            

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", buyerId.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _orderRepository.GetOrderAsync(id).Returns(order);

            int saveChangesAsyncCounter = 0;
            int removeOrderAsyncCounter = 0;

            _orderRepository.When(x => x.RemoveOrder(Arg.Any<Order>()))
            .Do(x => removeOrderAsyncCounter++);

            _orderRepository.When(async x => await x.SaveChangesAsync())
            .Do(x => saveChangesAsyncCounter++);

            // Act

            var returnedOrderDTOResponse = await _sut.DeleteOrder(id);

            // Assert

            Assert.Equal(order.BuyerId, returnedOrderDTOResponse.BuyerId);
            Assert.Equal(order.Id, returnedOrderDTOResponse.Id);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, removeOrderAsyncCounter);
        }
    }
}
