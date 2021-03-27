namespace DetroitHarps.Api.Test.Middleware
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using DetroitHarps.Api.Middleware;
    using DetroitHarps.Business.Common.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    public class ExceptionHandlingMiddlewareTest
    {
        private readonly Mock<ILogger<ExceptionHandlingMiddleware>> _loggerMock;

        public ExceptionHandlingMiddlewareTest()
        {
            _loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        }

        [Fact]
        public void ThrowsOnNullRequestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ExceptionHandlingMiddleware(null, _loggerMock.Object));
        }

        [Fact]
        public void ThrowsOnNullLoggerTest()
        {
            RequestDelegate requestDelegate = (innerHttpContext) => innerHttpContext.Response.WriteAsync("test");

            Assert.Throws<ArgumentNullException>(() => new ExceptionHandlingMiddleware(requestDelegate, null));
        }

        [Fact]
        public async void MiddlewareShouldCatchBusinessExceptionsWithBadRequestAndMessageTest()
        {
            var message = Guid.NewGuid().ToString();

            RequestDelegate requestDelegate = (innerHttpContext) => throw new BusinessException(message);
            var middleware = new ExceptionHandlingMiddleware(requestDelegate, _loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8))
            {
                var streamText = reader.ReadToEnd();
                Assert.Equal(message, streamText);
            }

            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
        }

        [Fact]
        public async void MiddlewareShouldCatchNonBusinessExceptionsWithInternalServerErrorAndNoResponse()
        {
            var message = Guid.NewGuid().ToString();

            RequestDelegate requestDelegate =
                (innerHttpContext) => throw new InvalidOperationException(message);
            var middleware = new ExceptionHandlingMiddleware(requestDelegate, _loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(context.Response.Body))
            {
                var streamText = reader.ReadToEnd();
                Assert.Empty(streamText);
            }

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            _loggerMock.Verify(
                x => x.Log<object>(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<object>(y => y.ToString() == message),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()),
                Times.Once());
        }
    }
}