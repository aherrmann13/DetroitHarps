namespace DetroitHarps.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

    [Route("[Controller]")]
    public class ScheduleController : Controller
    {
        private IScheduleManager _scheduleManager;

        public ScheduleController(IScheduleManager scheduleManager)
        {
            Guard.NotNull(scheduleManager, nameof(scheduleManager));

            _scheduleManager = scheduleManager;
        }

        [HttpPost("Create")]
        [SwaggerOperation(OperationId = "CreateEvent")]
        public ActionResult<int> Create([FromBody] EventCreateModel model)
        {
            var result = _scheduleManager.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        [SwaggerOperation(OperationId = "UpdateEvent")]
        public ActionResult Update([FromBody] EventModel model)
        {
            _scheduleManager.Update(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(OperationId = "DeleteEvent")]
        public ActionResult Delete([FromRoute] int id)
        {
            _scheduleManager.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        [SwaggerOperation(OperationId = "GetAllEvents")]
        public ActionResult<IEnumerable<EventModel>> GetAll()
        {
            var result = _scheduleManager.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("Get")]
        [SwaggerOperation(OperationId = "GetEvents")]
        public ActionResult<IEnumerable<EventModel>> GetUpcoming(
            [FromQuery] DateTime? until = null)
        {
            var result = _scheduleManager.GetUpcoming(until);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetRegistration")]
        [SwaggerOperation(OperationId = "GetRegistrationEvents")]
        public ActionResult<IEnumerable<EventModel>> GetUpcomingRegistrationEvents()
        {
            var result = _scheduleManager.GetUpcomingRegistrationEvents();
            return Ok(result);
        }
    }
}