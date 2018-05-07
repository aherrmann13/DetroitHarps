namespace Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Tools;
    
    [Route("[Controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleManager _manager;

        public ScheduleController(IScheduleManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        [Produces("application/json", Type = typeof(IList<EventReadModel>))]
        [SwaggerOperation(operationId: "GetAllEvents")]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll().ToList();

            return Json(response);
        }

        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        [Produces("application/json", Type = typeof(EventReadModel))]
        [SwaggerOperation(operationId: "GetEvent")]
        public IActionResult Get(int id)
        {
            var response = _manager.Get(id);

            return Json(response);
        }
    }
}