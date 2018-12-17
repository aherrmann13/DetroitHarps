namespace DetroitHarps.Api.Middleware
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using DetroitHarps.Business.Exception;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Tools;

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            Guard.NotNull(next, nameof(next));
            Guard.NotNull(logger, nameof(logger));

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var swatch = Stopwatch.StartNew();
            _logger.LogInformation(GetPrerequestLog(context));
            await _next.Invoke(context).ConfigureAwait(false);
            swatch.Stop();
            _logger.LogInformation(GetPostrequestLog(context, swatch.ElapsedMilliseconds));
        }

        private static string GetPrerequestLog(HttpContext context)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append(FormatStringProperty("id", context.TraceIdentifier) + ", ");
            sb.Append(FormatStringProperty("request", context.Request.Path) + ", ");
            sb.Append(FormatNumberProperty("size", (context.Request.ContentLength ?? 0).ToString()));
            sb.Append("}");
            return sb.ToString();
        }

        private static string GetPostrequestLog(HttpContext context, long time)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append(FormatStringProperty("id", context.TraceIdentifier) + ", ");
            sb.Append(FormatStringProperty("request", context.Request.Path) + ", ");
            sb.Append(FormatNumberProperty("time", time.ToString()));
            sb.Append("}");
            return sb.ToString();
        }

        private static string FormatStringProperty(string key, string value)
        {
            return $"\"{key}\": \"{value}\"";
        }

        private static string FormatNumberProperty(string key, string value)
        {
            return $"\"{key}\": {value}";
        }
    }
}