using System.Net;

namespace Domain.Exceptions
{
    public class InvalidRequestArgumentsException : HttpResponseException
    {
        public InvalidRequestArgumentsException(string message) : base(HttpStatusCode.BadRequest, message) { }

    }
}
