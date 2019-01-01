namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.AspNetCore.Mvc;
    using Tools;

    [Route("[Controller]")]
    public class RegistrationController : Controller
    {
        private IRegistrationManager _registrationManager;

        public RegistrationController(IRegistrationManager registrationManager)
        {
            Guard.NotNull(registrationManager, nameof(registrationManager));

            _registrationManager = registrationManager;
        }

        [HttpPost("Register")]
        public ActionResult Register([FromBody] RegisterModel model)
        {
            _registrationManager.Register(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _registrationManager.Delete(id);
            return Ok();
        }

        [HttpGet("GetAllParents")]
        public ActionResult<IEnumerable<RegisteredParentModel>> GetAllParents()
        {
            var result = _registrationManager.GetAllRegisteredParents();
            return Ok(result);
        }

        [HttpGet("GetAllChildren")]
        public ActionResult<IEnumerable<RegisteredChildModel>> GetAllChildren()
        {
            var result = _registrationManager.GetAllRegisteredChildren();
            return Ok(result);
        }
    }
}