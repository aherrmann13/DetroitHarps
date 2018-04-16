namespace Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Mvc;
    using Service.Models;
    using Swashbuckle.AspNetCore;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Tools;

    [Route("[Controller]")]
    public class ContactController : Controller
    {
        IContactManager _manager;

        public ContactController(IContactManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [HttpPost("Contact")]
        [Produces("application/json", Type = typeof(void))]
        [SwaggerOperation(operationId: "Contact")]
        public IActionResult Contact([FromBody] ContactModel model)
        {
            _manager.Contact(model);

            return Ok();
        }
    }
}