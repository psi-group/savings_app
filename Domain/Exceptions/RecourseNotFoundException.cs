using System.Net;

namespace Domain.Exceptions
{
    public class RecourseNotFoundException : HttpResponseException
    {
        public RecourseNotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }

    }
}
