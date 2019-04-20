namespace DetroitHarps.Api.Controllers
{
    using DetroitHarps.Api.Services.ClientLogging;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

    [Route("[Controller]")]
    public class ClientLoggingController : Controller
    {
        private ClientLoggerFacade _clientLoggerFacade;

        public ClientLoggingController(ClientLoggerFacade clientLoggerFacade)
        {
            Guard.NotNull(clientLoggerFacade, nameof(clientLoggerFacade));

            _clientLoggerFacade = clientLoggerFacade;
        }

        [HttpPost("Error")]
        [SwaggerOperation(OperationId = "ClientError")]
        [AllowAnonymous]
        public ActionResult Contact([FromBody] ClientErrorModel model)
        {
            _clientLoggerFacade.LogError(model);
            return Ok();
        }
    }
}