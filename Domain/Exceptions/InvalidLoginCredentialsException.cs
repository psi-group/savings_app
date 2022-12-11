using System.Net;

namespace Domain.Exceptions
{
    public class InvalidLoginCredentialsException : HttpResponseException
    {
        public InvalidLoginCredentialsException(string message) : base(HttpStatusCode.BadRequest, message) { }

    }
}
