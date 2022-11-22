
using NSubstitute;
using savings_app_backend.EmailSender;
using savings_app_backend.Events;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class ProductTests
    {
        [Fact]
        public void ReduceAmount_ShouldThrowNotEnoughProductAmountException_WhenAmountToReduceExceedsAmountOfUnits()
        {
            //Arrange

            var product = new Product();
            product.AmountOfUnits = 1;

            //Act

            //Assert

            Assert.Throws<NotEnoughProductAmountException>(() => product.ReduceAmount(2));
        }

        [Fact]
        public void ReduceAmount_ShouldInvokeProductSoldEvent_WhenAmountOfUnitsExceedAmount()
        {

            //Arrange

            bool productSoldEventCalled = false;


            var userAuth = new UserAuth();
            userAuth.Email = "justasmileika@gmail.com";

            var restaurant = new Restaurant();
            restaurant.UserAuth = userAuth;

            var product = new Product();
            product.AmountOfUnits = 2;
            product.Restaurant = restaurant;


            int amountToReduce = 1;

            var emailSender = Substitute.For<IEmailSender>();

            product.ProductSold += emailSender.NotifyRestaurantSoldProduct;

            emailSender.When(x => x.NotifyRestaurantSoldProduct(Arg.Any<Object>(), Arg.Any<ProductSoldEventArgs>()))
                .Do(x => { productSoldEventCalled = true; } );

            //Act

            product.ReduceAmount(amountToReduce);

            //Assert

            Assert.True(productSoldEventCalled);
            Assert.Equal(1, product.AmountOfUnits);
        }

        [Fact]
        public void ReduceAmount_ShouldInvokeProductSoldOutEvent_WhenAmountOfUnitsIsEqualToAmount()
        {

            //Arrange

            bool productSoldOutEventCalled = false;


            var userAuth = new UserAuth();
            userAuth.Email = "justasmileika@gmail.com";

            var restaurant = new Restaurant();
            restaurant.UserAuth = userAuth;

            var product = new Product();
            product.AmountOfUnits = 2;
            product.Restaurant = restaurant;


            int amountToReduce = 2;

            var emailSender = Substitute.For<IEmailSender>();

            product.ProductSoldOut += emailSender.NotifyRestaurantSoldOutProduct;

            emailSender.When(x => x.NotifyRestaurantSoldOutProduct(product, product.Restaurant.UserAuth.Email))
                .Do(x => { productSoldOutEventCalled = true; });

            //Act

            product.ReduceAmount(amountToReduce);

            //Assert

            Assert.True(productSoldOutEventCalled);
            Assert.Equal(0, product.AmountOfUnits);
        }
    }
}
