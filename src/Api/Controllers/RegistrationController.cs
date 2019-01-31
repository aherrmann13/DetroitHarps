namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetAllParents/Csv")]
        [Produces(CsvContentType)]
        public ActionResult GetAllParentsCsv()
        {
            var fileBytes = _csvWriter.GetAsCsv(
                _registrationManager.GetAllRegisteredParents().ToList());
            return File(fileBytes, CsvContentType, GetAllParentsCsvName);
        }

        [HttpGet("GetAllChildren/Csv")]
        [Produces(CsvContentType)]
        public ActionResult GetAllChildrenCsv()
        {
            var fileBytes = _csvWriter.GetAsCsv(
                _registrationManager.GetAllRegisteredChildren().ToList());
            return File(fileBytes, CsvContentType, GetAllChildrenCsvName);
        }
    }
}