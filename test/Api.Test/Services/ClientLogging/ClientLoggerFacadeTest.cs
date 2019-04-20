namespace DetroitHarps.Api.Services.ClientLogging.Test
{
    using System;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class ClientLoggerFacadeTest
    {
        private readonly ClientLoggerFacade _loggerFacade;
        private readonly Mock<ILogger<ClientLoggerFacade>> _loggerMock;

        public ClientLoggerFacadeTest()
        {
            _loggerMock = new Mock<ILogger<ClientLoggerFacade>>();
            _loggerFacade = new ClientLoggerFacade(_loggerMock.Object);
        }

        [Fact]
        public void LogsErrorWithCorrectMessageTest()
        {
            var error = new ClientErrorModel
            {
                Timestamp = new DateTime(2019, 4, 20, 12, 52, 35, DateTimeKind.Utc),
                SessionId = "session",
                Message = "message"
            };
            _loggerFacade.LogError(error);

            var expectedMessage = "[2019-04-20T12:52:35.0000000Z] [session] message";

            _loggerMock.Verify(
                x => x.Log<object>(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<object>(y => y.ToString() == expectedMessage),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()),
                Times.Once());
        }
    }
}