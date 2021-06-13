namespace DetroitHarps.Api.Controllers
{
    using DetroitHarps.Business.Registration;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

    [Route("[Controller]")]
    public class RegistrationCsvController : Controller
    {
        private const string CsvContentType = "text/csv";
        private const string GetParentsCsvName = "parents.csv";
        private const string GetChildrenCsvName = "children.csv";

        private readonly IRegistrationCsvManager _registrationCsvManager;

        public RegistrationCsvController(IRegistrationCsvManager registrationCsvManager)
        {
            Guard.NotNull(registrationCsvManager, nameof(registrationCsvManager));

            _registrationCsvManager = registrationCsvManager;
        }

        [HttpGet("GetParents/{year}")]
        [SwaggerOperation(OperationId = "GetParentsCsv")]
        [Produces(CsvContentType, Type = typeof(FileResult))]
        public FileResult GetParentsCsv([FromRoute] int year)
        {
            var fileBytes = _registrationCsvManager.GetRegisteredParents(year);
            return File(fileBytes, CsvContentType, GetParentsCsvName);
        }

        [HttpGet("GetChildren/{year}")]
        [SwaggerOperation(OperationId = "GetChildrenCsv")]
        [Produces(CsvContentType, Type = typeof(FileResult))]
        public FileResult GetChildrenCsv([FromRoute] int year)
        {
            var fileBytes = _registrationCsvManager.GetRegisteredChildren(year);
            return File(fileBytes, CsvContentType, GetChildrenCsvName);
        }
    }
}