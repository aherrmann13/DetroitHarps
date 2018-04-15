namespace Service.Middleware
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class LoggingMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;
        readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var swatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0}) bytes");
                await _next.Invoke(httpContext).ConfigureAwait(false);
                swatch.Stop();
                _logger.LogInformation($"Request for {httpContext.Request.Path} took ({swatch.ElapsedMilliseconds}) ms");

                var statusCode = httpContext.Response?.StatusCode;
            }
            catch (Exception ex) 
            {
                if(httpContext.Response.StatusCode == StatusCodes.Status200OK)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
                _logger.LogInformation($"Request for {httpContext.Request.Path} resulted in error");
                _logger.LogError(ex.ToString());
            }
        }
    }
}