using System.Net;

namespace Domain.Exceptions
{
    public class InvalidEmailException : HttpResponseException
    {
        public InvalidEmailException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
