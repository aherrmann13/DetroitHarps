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
        private const string MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

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
                _logger.LogDebug($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0} bytes");
                await _next(httpContext);
                swatch.Stop();
                _logger.LogDebug($"request took {swatch.ElapsedMilliseconds} ms");

                var statusCode = httpContext.Response?.StatusCode;
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) 
            { 
                _logger.LogError(null, ex);
            }
        }
    }
}