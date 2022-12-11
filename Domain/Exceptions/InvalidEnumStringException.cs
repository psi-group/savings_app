using System.Net;

namespace Domain.Exceptions
{
    public class InvalidEnumStringException : HttpResponseException
    {
        public InvalidEnumStringException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
