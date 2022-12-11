using System.Net;

namespace Domain.Exceptions
{
    public class RecourseAlreadyExistsException : HttpResponseException
    {
        public RecourseAlreadyExistsException(string message) : base(HttpStatusCode.BadRequest, message) { }

    }
}
