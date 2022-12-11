using System.Net;

namespace Domain.Exceptions
{
    public class InvalidRoleException : HttpResponseException
    {
        public InvalidRoleException(string message) : base(HttpStatusCode.Unauthorized, message) { }

    }
}
