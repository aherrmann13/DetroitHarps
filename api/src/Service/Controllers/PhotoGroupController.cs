namespace Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Tools;

    [Route("[Controller]")]
    public class PhotoGroupController : Controller
    {
        IPhotoGroupManager _manager;

        public PhotoGroupController(IPhotoGroupManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [AllowAnonymous]
        [HttpGet("Get")]
        [Produces("application/json", Type = typeof(IList<PhotoGroupReadModel>))]
        [SwaggerOperation(operationId: "GetAllPhotoGroups")]
        public IActionResult Get()
        {
            var response = _manager.Get().ToList();

            return Json(response);
        }

        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        [Produces("application/json", Type = typeof(PhotoGroupReadModel))]
        [SwaggerOperation(operationId: "GetPhotoGroup")]
        public IActionResult Get(int id)
        {
            var response = _manager.Get(id);

            return Json(response);
        }
    }
}