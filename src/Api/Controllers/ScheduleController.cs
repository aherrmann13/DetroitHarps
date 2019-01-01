namespace DetroitHarps.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Models;
    using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<int> Create([FromBody] EventCreateModel model)
        {
            var result = _scheduleManager.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] EventModel model)
        {
            _scheduleManager.Update(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _scheduleManager.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<EventModel>> GetAll()
        {
            var result = _scheduleManager.GetAll();
            return Ok(result);
        }

        [HttpGet("Get")]
        public ActionResult<IEnumerable<EventModel>> GetUpcoming(
            [FromQuery] DateTime? until = null)
        {
            var result = _scheduleManager.GetUpcoming(until);
            return Ok(result);
        }
    }
}