namespace DetroitHarps.Api.Services.ClientLogging
{
    using Microsoft.Extensions.Logging;
    using Tools;

    public class ClientLoggerFacade
    {
        private readonly ILogger<ClientLoggerFacade> _logger;

        public ClientLoggerFacade(ILogger<ClientLoggerFacade> logger)
        {
            Guard.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        public void LogError(ClientErrorModel error)
        {
            _logger.LogError(FormatError(error));
        }

        private string FormatError(ClientErrorModel error)
        {
            return $"[{error.Timestamp:o}] [{error.SessionId}] {error.Message}";
        }
    }
}