namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;
    using Tools.Csv;

    [Route("[Controller]")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationManager _registrationManager;

        public RegistrationController(IRegistrationManager registrationManager)
        {
            Guard.NotNull(registrationManager, nameof(registrationManager));

            _registrationManager = registrationManager;
        }

        [HttpPost("Register")]
        [SwaggerOperation(OperationId = "Register")]
        [AllowAnonymous]
        public ActionResult Register([FromBody] RegisterModel model)
        {
            _registrationManager.Register(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(OperationId = "DeleteRegistration")]
        public ActionResult Delete([FromRoute] int id)
        {
            _registrationManager.Delete(id);
            return Ok();
        }

        [HttpDelete("DeleteChild/{id}/{firstName}/{lastName}")]
        [SwaggerOperation(OperationId = "DeleteRegisteredChild")]
        public ActionResult Delete(
            [FromRoute] int id,
            [FromRoute] string firstName,
            [FromRoute] string lastName)
        {
            _registrationManager.DeleteChild(id, firstName, lastName);
            return Ok();
        }

        [HttpGet("GetAllParents")]
        [SwaggerOperation(OperationId = "GetAllParents")]
        public ActionResult<IEnumerable<RegisteredParentModel>> GetAllParents()
        {
            var result = _registrationManager.GetAllRegisteredParents();
            return Ok(result);
        }

        [HttpGet("GetAllChildren")]
        [SwaggerOperation(OperationId = "GetAllChildren")]
        public ActionResult<IEnumerable<RegisteredChildModel>> GetAllChildren()
        {
            var result = _registrationManager.GetAllRegisteredChildren();
            return Ok(result);
        }
    }
}