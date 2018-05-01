namespace Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Tools;

    [Route("[Controller]")]
    public class RegistrationController : Controller
    {
        IRegistrationManager _manager;

        public RegistrationController(IRegistrationManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [HttpPost("Register")]
        [Produces("application/json", Type = typeof(int))]
        [SwaggerOperation(operationId: "Register")]
        public IActionResult Register([FromBody] RegistrationCreateModel model)
        {
            var response = _manager.Register(model);

            return Json(response);
        }
    }
}