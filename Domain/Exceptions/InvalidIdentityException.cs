using System.Net;

namespace Domain.Exceptions
{
    public class InvalidIdentityException : HttpResponseException
    {
        public InvalidIdentityException(string message) : base(HttpStatusCode.Unauthorized, message) { }

    }
}
