namespace DetroitHarps.Api.Test.Middleware
{
    using System;
    using System.Threading.Tasks;
    using DetroitHarps.Api.Middleware;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class RequestLoggingMiddlewareTest
    {
        private readonly Mock<ILogger<RequestLoggingMiddleware>> _loggerMock;

        public RequestLoggingMiddlewareTest()
        {
            _loggerMock = new Mock<ILogger<RequestLoggingMiddleware>>();
        }

        [Fact]
        public void ThrowsOnNullRequestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestLoggingMiddleware(null, _loggerMock.Object));
        }

        [Fact]
        public void ThrowsOnNullLoggerTest()
        {
            RequestDelegate requestDelegate = (innerHttpContext) => innerHttpContext.Response.WriteAsync("test");

            Assert.Throws<ArgumentNullException>(() => new RequestLoggingMiddleware(requestDelegate, null));
        }

        [Fact]
        public async void PrerequestLogFormattedCorrectlyTest()
        {
            RequestDelegate requestDelegate = (innerHttpContext) => Task.CompletedTask;
            var middleware = new RequestLoggingMiddleware(requestDelegate, _loggerMock.Object);

            var context = new DefaultHttpContext();
            var path = "/test";
            var size = 50;
            context.Request.Path = new PathString(path);
            context.Request.ContentLength = size;

            await middleware.Invoke(context);

            var expectedMessage = $"{{\"id\": \"{context.TraceIdentifier}\", " +
                $"\"request\": \"{path}\", \"size\": {size}}}";
            _loggerMock.Verify(
                x => x.Log<object>(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<object>(y => y.ToString() == expectedMessage),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async void PostrequestLogFormattedCorrectlyTest()
        {
            RequestDelegate requestDelegate = (innerHttpContext) => Task.CompletedTask;
            var middleware = new RequestLoggingMiddleware(requestDelegate, _loggerMock.Object);

            var context = new DefaultHttpContext();
            var path = "/test";
            var size = 50;
            context.Request.Path = new PathString(path);
            context.Request.ContentLength = size;

            await middleware.Invoke(context);

            // TODO: find way to test timer
            var expectedStart = $"{{\"id\": \"{context.TraceIdentifier}\", " +
                $"\"request\": \"{path}\", \"time\": ";
            _loggerMock.Verify(
                x => x.Log<object>(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<object>(y => y.ToString().StartsWith(expectedStart)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()),
                Times.Once());
        }
    }
}