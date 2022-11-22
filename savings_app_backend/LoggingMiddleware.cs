using System.Text;

namespace savings_app_backend
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
                if(httpContext.Response.StatusCode != 200)
                {
                    /*using(var reader = new StreamReader(
                        httpContext.Response.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        bufferSize: 1024,
                        leaveOpen: true))
                    {
                        string responseBody = await reader.ReadToEndAsync();

                        httpContext.Response.Body.Position = 0;

                        _logger.LogError(responseBody);
                    }*/
                    

                }
            }
            catch(Exception e)
            {
                //_logger.LogError(e.ToString());
                throw;
            }
        }
    }

    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
