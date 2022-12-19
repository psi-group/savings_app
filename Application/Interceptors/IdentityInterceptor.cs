using Castle.DynamicProxy;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Interceptors
{
    public class IdentityInterceptor : IInterceptor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        

        public IdentityInterceptor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Intercept(IInvocation invocation)
        {
            var isProtectedByPrivateIdentity = invocation.Method.CustomAttributes.Any(a => a.AttributeType == typeof(PrivateIdentityAttribute));

            if(isProtectedByPrivateIdentity)
            {
                var id = (Guid)invocation.Arguments[0];
                if (id != Guid.Parse(((ClaimsIdentity)_contextAccessor.HttpContext.User.Identity).FindFirst("Id").Value))
                    throw new InvalidIdentityException("you are unauthorized to access this recource");
            }

            invocation.Proceed();
        }
    }
}
