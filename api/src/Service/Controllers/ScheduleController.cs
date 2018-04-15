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
    public class ScheduleController : Controller
    {
        private readonly IScheduleManager _manager;

        public ScheduleController(IScheduleManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [HttpPost("Create")]
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Create")]
        public IActionResult Create([FromBody] EventCreateModel[] models)
        {
            var response = _manager.Create(models).ToList();

            return Json(response);
        }

        [HttpPost("Update")]
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Update")]
        public IActionResult Update([FromBody] EventUpdateModel[] models)
        {
            var response = _manager.Update(models).ToList();

            return Json(response);
        }

        [HttpPost("Delete")]
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Delete")]
        public IActionResult Delete([FromBody] int[] ids)
        {
            var response = _manager.Delete(ids).ToList();

            return Json(response);
        }

        [HttpGet("GetAll")]
        [Produces("application/json", Type = typeof(IList<EventReadModel>))]
        [SwaggerOperation(operationId: "GetAll")]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll().ToList();

            return Json(response);
        }

        // TODO : post? (prefer ids in body)
        // ie caps query length although not
        // expecting that number of events
        [HttpGet("Get")]
        [Produces("application/json", Type = typeof(IList<EventReadModel>))]
        [SwaggerOperation(operationId: "Get")]
        public IActionResult Get([FromQuery] int[] ids)
        {
            var response = _manager.Get(ids).ToList();

            return Json(response);
        }
    }
}