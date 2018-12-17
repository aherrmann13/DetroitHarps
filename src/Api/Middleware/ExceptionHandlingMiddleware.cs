namespace DetroitHarps.Api.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using DetroitHarps.Business.Exception;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Tools;

    public class ExceptionHandlingMiddleware
    {
        private const string ErrorResponseMessage = "Error occurred during request";

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            Guard.NotNull(next, nameof(next));
            Guard.NotNull(logger, nameof(logger));

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is BusinessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(exception.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(exception.Message, exception);

                return Task.CompletedTask;
            }
        }
    }
}