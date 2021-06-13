namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

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

        [HttpGet("GetParents/{year}")]
        [SwaggerOperation(OperationId = "GetParents")]
        public ActionResult<IEnumerable<RegisteredParentModel>> GetParents([FromRoute] int year)
        {
            var result = _registrationManager.GetRegisteredParents(year);
            return Ok(result);
        }

        [HttpGet("GetChildren/{year}")]
        [SwaggerOperation(OperationId = "GetChildren")]
        public ActionResult<IEnumerable<RegisteredChildModel>> GetChildren([FromRoute] int year)
        {
            var result = _registrationManager.GetRegisteredChildren(year);
            return Ok(result);
        }
    }
}