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
        private const string CsvContentType = "text/csv";
        private const string GetAllParentsCsvName = "parents.csv";
        private const string GetAllChildrenCsvName = "children.csv";

        private readonly IRegistrationManager _registrationManager;
        private readonly ICsvWriter _csvWriter;

        public RegistrationController(
            IRegistrationManager registrationManager,
            ICsvWriter csvWriter)
        {
            Guard.NotNull(registrationManager, nameof(registrationManager));
            Guard.NotNull(csvWriter, nameof(csvWriter));

            _registrationManager = registrationManager;
            _csvWriter = csvWriter;
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

        [HttpGet("GetAllParents/Csv")]
        [SwaggerOperation(OperationId = "GetAllParentsCsv")]
        [Produces(CsvContentType)]
        public ActionResult GetAllParentsCsv()
        {
            var fileBytes = _csvWriter.GetAsCsv(
                _registrationManager.GetAllRegisteredParents().ToList());
            return File(fileBytes, CsvContentType, GetAllParentsCsvName);
        }

        [HttpGet("GetAllChildren/Csv")]
        [SwaggerOperation(OperationId = "GetAllChildrenCsv")]
        [Produces(CsvContentType)]
        public ActionResult GetAllChildrenCsv()
        {
            var fileBytes = _csvWriter.GetAsCsv(
                _registrationManager.GetAllRegisteredChildren().ToList());
            return File(fileBytes, CsvContentType, GetAllChildrenCsvName);
        }
    }
}