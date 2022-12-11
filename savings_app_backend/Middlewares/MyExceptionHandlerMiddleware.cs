

using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class MyExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyExceptionHandlerMiddleware> _logger;

        public MyExceptionHandlerMiddleware(RequestDelegate next, ILogger<MyExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleExceptionsAsync(context, ex);                
            }
        }

        public async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Unexpected internal server error";

            if(ex is HttpResponseException)
            {
                statusCode = ((HttpResponseException)ex).StatusCode;
                message = ex.Message;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails();

            problemDetails.Status = (int)statusCode;
            problemDetails.Title = message;

            var result = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(result);

        }
    }
}
