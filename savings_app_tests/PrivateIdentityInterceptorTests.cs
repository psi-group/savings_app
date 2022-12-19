
using Application;
using Application.Interceptors;
using Castle.DynamicProxy;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Org.BouncyCastle.Asn1.Crmf;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace savings_app_tests
{
    public class PrivateIdentityInterceptorTests
    {
        class MockCustomAttributeData : CustomAttributeData
        {
            
        }
        [Fact]
        public void Intercept_ShouldThrowInvalidIdentityException_WhenIdentityDoesntMatchIdArgumentAndMethodIsProtectedByPrivateIdentity()
        {
            //Arrange
            
            
            IInvocation invocation = Substitute.For<IInvocation>();

            MethodInfo method = Substitute.For<MethodInfo>();

            invocation.Method.Returns(method);


            var customAttributeData = Substitute.For<CustomAttributeData>();


            //var customAtrributeData = new MockCustomAttributeData();

            customAttributeData.AttributeType.Returns(typeof(PrivateIdentityAttribute));

            var customArguments = new List<CustomAttributeData>() { customAttributeData };

            invocation.Method.CustomAttributes.Returns(customArguments);

            //var arg = ;

            //invocation.Method.CustomAttributes.Any<CustomAttributeData>(Arg.Any<Func<object, bool>>()).ReturnsForAnyArgs(x => true);

            invocation.Arguments.Returns(new object[] {default(Guid)});


            var _httpContext = Substitute.For<IHttpContextAccessor>();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", Guid.NewGuid().ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            

        //Act



        //Assert

        Assert.Throws<InvalidIdentityException>(() => (new IdentityInterceptor(_httpContext)).Intercept(invocation));
        }

        [Fact]
        public void Intercept_ShouldProceed_WhenIdentityMatchesIdArgumentAndMethodIsProtectedByPrivateIdentity()
        {
            //Arrange


            IInvocation invocation = Substitute.For<IInvocation>();

            MethodInfo method = Substitute.For<MethodInfo>();

            invocation.Method.Returns(method);


            var customAttributeData = Substitute.For<CustomAttributeData>();


            //var customAtrributeData = new MockCustomAttributeData();

            customAttributeData.AttributeType.Returns(typeof(PrivateIdentityAttribute));

            var customArguments = new List<CustomAttributeData>() { customAttributeData };

            invocation.Method.CustomAttributes.Returns(customArguments);

            //var arg = ;

            //invocation.Method.CustomAttributes.Any<CustomAttributeData>(Arg.Any<Func<object, bool>>()).ReturnsForAnyArgs(x => true);

            invocation.Arguments.Returns(new object[] { default(Guid) });


            var _httpContext = Substitute.For<IHttpContextAccessor>();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            int proceedCounter = 0;

            invocation.When(x => x.Proceed())
                .Do(x => proceedCounter++);


            //Act

            new IdentityInterceptor(_httpContext).Intercept(invocation);

            //Assert

            Assert.Equal(1, proceedCounter);
        }


        [Fact]
        public void Intercept_ShouldProceed_WhenMethodIsNotProtectedByPrivateIdentity()
        {
            //Arrange


            IInvocation invocation = Substitute.For<IInvocation>();

            MethodInfo method = Substitute.For<MethodInfo>();

            invocation.Method.Returns(method);


            var customAttributeData = Substitute.For<CustomAttributeData>();


            //var customAtrributeData = new MockCustomAttributeData();

            customAttributeData.AttributeType.Returns(typeof(PrivateIdentityAttribute));

            var customArguments = new List<CustomAttributeData>() {  };

            invocation.Method.CustomAttributes.Returns(customArguments);

            //var arg = ;

            //invocation.Method.CustomAttributes.Any<CustomAttributeData>(Arg.Any<Func<object, bool>>()).ReturnsForAnyArgs(x => true);

            var _httpContext = Substitute.For<IHttpContextAccessor>();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", Guid.NewGuid().ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            int proceedCounter = 0;

            invocation.When(x => x.Proceed())
                .Do(x => proceedCounter++);


            //Act

            new IdentityInterceptor(_httpContext).Intercept(invocation);

            //Assert

            Assert.Equal(1, proceedCounter);
        }

    }
}
