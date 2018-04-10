namespace Service.Controllers
{
    using System.Collections.Generic;
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
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Register")]
        public IActionResult Register([FromBody] RegistrationCreateModel[] models)
        {
            var response = _manager.Register(models);

            return Json(response);
        }

        [HttpPost("GetAll")]
        [Produces("application/json", Type = typeof(IList<RegistrationReadModel>))]
        [SwaggerOperation(operationId: "GetAll")]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll();

            return Json(response);
        }

        [HttpPost("GetAllChildren")]
        [Produces("application/json", Type = typeof(IList<ChildInformationReadModel>))]
        [SwaggerOperation(operationId: "GetAllChildren")]
        public IActionResult GetAllChildren()
        {
            var response = _manager.GetAllChildren();

            return Json(response);
        }
    }
}