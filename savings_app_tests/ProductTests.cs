
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace savings_app_tests
{
    public class ProductTests
    {
        [Fact]
        public void ReduceAmount_ShouldThrowNotEnoughProductAmountException_WhenAmountToReduceExceedsAmountOfUnits()
        {
            //Arrange

            var product = new Product(
                Guid.NewGuid(),
                "name",
                0,
                Guid.NewGuid(),
                null,
                0,
                0,
                1,
                0,
                null,
                DateTime.Now,
                null);

            //Act

            //Assert

            Assert.Throws<NotEnoughProductAmountException>(() => product.Buy(2, Guid.NewGuid()));
        }

        [Fact]
        public void ReduceAmount_ShouldInvokeProductSoldEvent_WhenAmountOfUnitsExceedAmount()
        {

            //Arrange

            bool productSoldEventCalled = false;


            var userAuth = new UserAuth(
                "password",
                "justasmileika@gmail.com"
                );

            var restaurant = new Restaurant(
                Guid.NewGuid(),
                "restaurant",
                userAuth,
                new Address(),
                null,
                false,
                null,
                null,
                null
                );

            var product = new Product(
                Guid.NewGuid(),
                "product",
                0,
                restaurant.Id,
                null,
                0,
                0,
                2,
                0,
                null,
                DateTime.Now,
                null
                );


            int amountToReduce = 1;

            var emailSender = Substitute.For<IEmailSender>();

            product.ProductSold += emailSender.NotifyRestaurantSoldProduct;

            emailSender.When(x => x.NotifyRestaurantSoldProduct(Arg.Any<Object>(), Arg.Any<ProductSoldEventArgs>()))
                .Do(x => { productSoldEventCalled = true; } );

            //Act

            product.Buy(amountToReduce, Guid.NewGuid());

            //Assert

            Assert.True(productSoldEventCalled);
            Assert.Equal(1, product.AmountOfUnits);
        }
        
        [Fact]
        public void ReduceAmount_ShouldInvokeProductSoldOutEvent_WhenAmountOfUnitsIsEqualToAmount()
        {

            //Arrange

            bool productSoldOutEventCalled = false;


            var userAuth = new UserAuth(
                "password",
                "justasmileika@gmail.com"
                );

            var restaurant = new Restaurant(
                Guid.NewGuid(),
                "restaurant",
                userAuth,
                new Address(),
                null,
                false,
                null,
                null,
                null
                );

            var product = new Product(
                Guid.NewGuid(),
                "product",
                0,
                restaurant.Id,
                null,
                0,
                0,
                2,
                0,
                null,
                DateTime.Now,
                null
                );


            int amountToReduce = 2;

            var emailSender = Substitute.For<IEmailSender>();

            product.ProductSoldOut += emailSender.NotifyRestaurantSoldOutProduct;

            emailSender.When(x => x.NotifyRestaurantSoldOutProduct(Arg.Any<Object>(), Arg.Any<ProductSoldOutEventArgs>()))
                .Do(x => { productSoldOutEventCalled = true; });

            //Act

            product.Buy(amountToReduce, Guid.NewGuid());

            //Assert

            Assert.True(productSoldOutEventCalled);
            Assert.Equal(0, product.AmountOfUnits);
        }
    }
}
