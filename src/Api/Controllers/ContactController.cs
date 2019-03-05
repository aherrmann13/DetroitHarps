namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

    [Route("[Controller]")]
    public class ContactController : Controller
    {
        private IContactManager _contactManager;

        public ContactController(IContactManager contactManager)
        {
            Guard.NotNull(contactManager, nameof(contactManager));

            _contactManager = contactManager;
        }

        [HttpPost("Contact")]
        [SwaggerOperation(OperationId = "Contact")]
        [AllowAnonymous]
        public ActionResult Contact([FromBody] MessageModel model)
        {
            _contactManager.Contact(model);
            return Ok();
        }

        [HttpPut("MarkAsRead/{id}")]
        [SwaggerOperation(OperationId = "MarkAsRead")]
        public ActionResult MarkAsRead([FromRoute] int id)
        {
            _contactManager.MarkAsRead(id);
            return Ok();
        }

        [HttpPut("MarkAsUnread/{id}")]
        [SwaggerOperation(OperationId = "MarkAsUnread")]
        public ActionResult MarkAsUnread([FromRoute] int id)
        {
            _contactManager.MarkAsUnread(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        [SwaggerOperation(OperationId = "GetAllMessages")]
        public ActionResult<IEnumerable<MessageReadModel>> GetAll()
        {
            var result = _contactManager.GetAll();
            return Ok(result);
        }
    }
}