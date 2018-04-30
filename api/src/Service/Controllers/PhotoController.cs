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
    public class PhotoController : Controller
    {
        IPhotoManager _manager;

        public PhotoController(IPhotoManager manager)
        {
            Guard.NotNull(manager, nameof(manager));

            _manager = manager;
        }

        [HttpGet("GetMetadata")]
        [Produces("application/json", Type = typeof(IList<PhotoMetadataReadModel>))]
        [SwaggerOperation(operationId: "GetAllPhotoMetadata")]
        public IActionResult GetMetadata()
        {
            var response = _manager.GetMetadata().ToList();

            return Json(response);
        }

        [HttpGet("GetMetadata/{id}")]
        [Produces("application/json", Type = typeof(PhotoMetadataReadModel))]
        [SwaggerOperation(operationId: "GetPhotoMetadata")]
        public IActionResult GetMetadata(int id)
        {
            var response = _manager.GetMetadata(id);

            return Json(response);
        }

        // TODO how to return this
        [HttpGet("Get/{id}")]
        //[Produces("application/json", Type = typeof(PhotoReadModel))]
        //[SwaggerOperation(operationId: "GetPhoto")]
        public IActionResult Get(int id)
        {
            var response = _manager.Get(id);

            // TODO image/jpeg or other type saved in db
            // this needs to be tested by swagger client
            return File(response.Photo, "image/jpeg");
        }
    }
}