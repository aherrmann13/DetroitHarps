namespace Microsoft.AspNetCore.Builder
{
    using DetroitHarps.Api.Middleware;
    using Microsoft.AspNetCore.Builder;
    using Tools;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder builder)
        {
            Guard.NotNull(builder, nameof(builder));

            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder AddRequestLogging(this IApplicationBuilder builder)
        {
            Guard.NotNull(builder, nameof(builder));

            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }

        public static IApplicationBuilder AddAuth0(this IApplicationBuilder builder)
        {
            Guard.NotNull(builder, nameof(builder));

            return builder.UseAuthentication();
        }
    }
}