namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Models;
    using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Contact([FromBody] MessageModel model)
        {
            _contactManager.Contact(model);
            return Ok();
        }

        [HttpPut("MarkAsRead/{id}")]
        public ActionResult MarkAsRead(int id)
        {
            _contactManager.MarkAsRead(id);
            return Ok();
        }

        [HttpPut("MarkAsUnread/{id}")]
        public ActionResult MarkAsUnread(int id)
        {
            _contactManager.MarkAsUnread(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<MessageReadModel>> GetAll()
        {
            var result = _contactManager.GetAll();
            return Ok(result);
        }
    }
}