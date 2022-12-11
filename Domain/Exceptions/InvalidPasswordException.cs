using System.Net;

namespace Domain.Exceptions
{
    public class InvalidPasswordException : HttpResponseException
    {
        public InvalidPasswordException(string message) : base(HttpStatusCode.BadRequest, message) { }

    }
}
