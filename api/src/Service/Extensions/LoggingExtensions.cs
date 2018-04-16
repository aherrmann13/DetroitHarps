namespace Microsoft.AspNetCore.Builder
{
    using Service.Middleware;

    public static class LoggingExtensions
    {
        public static IApplicationBuilder UseCustomLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}