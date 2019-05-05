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
    public class RegistrationCsvController : Controller
    {
        private const string CsvContentType = "text/csv";
        private const string GetAllParentsCsvName = "parents.csv";
        private const string GetAllChildrenCsvName = "children.csv";

        private readonly IRegistrationCsvManager _registrationCsvManager;

        public RegistrationCsvController(IRegistrationCsvManager registrationCsvManager)
        {
            Guard.NotNull(registrationCsvManager, nameof(registrationCsvManager));

            _registrationCsvManager = registrationCsvManager;
        }

        [HttpGet("GetAllParents")]
        [SwaggerOperation(OperationId = "GetAllParentsCsv")]
        [Produces(CsvContentType, Type = typeof(FileResult))]
        public FileResult GetAllParentsCsv()
        {
            var fileBytes = _registrationCsvManager.GetAllRegisteredParents();
            return File(fileBytes, CsvContentType, GetAllParentsCsvName);
        }

        [HttpGet("GetAllChildren")]
        [SwaggerOperation(OperationId = "GetAllChildrenCsv")]
        [Produces(CsvContentType, Type = typeof(FileResult))]
        public FileResult GetAllChildrenCsv()
        {
            var fileBytes = _registrationCsvManager.GetAllRegisteredChildren();
            return File(fileBytes, CsvContentType, GetAllChildrenCsvName);
        }
    }
}