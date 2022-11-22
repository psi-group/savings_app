using static System.Net.WebRequestMethods;

namespace savings_app_backend.Statistics
{
    public class StatisticsMiddleware
    {
        private readonly RequestDelegate next;
        

        public StatisticsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            lock(Statistics.RequestStatistics)
            {
                if (!Statistics.RequestStatistics.ContainsKey(httpContext.Request.Path))
                {
                    Statistics.RequestStatistics[httpContext.Request.Path] = 0;
                }
                Statistics.RequestStatistics[httpContext.Request.Path]++;
                
            }
            await next(httpContext);
        }
    }

    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseStatisticsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StatisticsMiddleware>();
        }
    }
}
