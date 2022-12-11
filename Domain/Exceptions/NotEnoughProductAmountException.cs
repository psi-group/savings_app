using System.Net;

namespace Domain.Exceptions
{
    public class NotEnoughProductAmountException : HttpResponseException
    {
        public NotEnoughProductAmountException(string message) : base(HttpStatusCode.BadRequest, message) { }

    }
}
