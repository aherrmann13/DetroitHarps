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
        }

        //// // TODO use data loader for now
        ////[HttpPost("Create")]
        ////[Produces("application/json", Type = typeof(int))]
        ////[SwaggerOperation(operationId: "Create")]
        ////public async Task<IActionResult> Create(PhotoUploadModel uploadModel)
        ////{
        ////    if(uploadModel == null)
        ////    {
        ////        throw new ArgumentNullException("null model posted");
        ////    }
        ////    var createModel = new PhotoCreateModel
        ////    {
        ////        Title = uploadModel.Title,
        ////        GroupId = uploadModel.GroupId,
        ////        SortOrder = uploadModel.SortOrder
        ////    };
        ////
        ////    //TODO validate file name
        ////
        ////    using (var memoryStream = new MemoryStream())
        ////    {
        ////        await uploadModel.Photo.CopyToAsync(memoryStream);
        ////        createModel.Photo = memoryStream.ToArray();
        ////    }
        ////
        ////    var response = _manager.Create(createModel);
        ////
        ////    return Json(response);
        ////}

        [HttpPost("Update")]
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Update")]
        public IActionResult Update([FromBody] PhotoMetadataUpdateModel[] models)
        {
            var response = _manager.Update(models).ToList();

            return Json(response);
        }

        // TODO : make this consistant with other deletes
        [HttpPost("Delete")]
        [Produces("application/json", Type = typeof(IList<int>))]
        [SwaggerOperation(operationId: "Delete")]
        public IActionResult Delete([FromBody] int[] ids)
        {
            var response = _manager.Delete(ids).ToList();

            return Json(response);
        }

        [HttpGet("GetAll")]
        [Produces("application/json", Type = typeof(IList<PhotoMetadataReadModel>))]
        [SwaggerOperation(operationId: "GetAll")]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll().ToList();

            return Json(response);
        }

        // TODO : post? (prefer ids in body)
        [HttpGet("Get")]
        [Produces("application/json", Type = typeof(IList<PhotoMetadataReadModel>))]
        [SwaggerOperation(operationId: "Get")]
        public IActionResult Get([FromQuery] int[] ids)
        {
            var response = _manager.Get(ids).ToList();

            return Json(response);
        }

        [HttpGet("Get/{id}")]
        [Produces("application/json", Type = typeof(PhotoReadModel))]
        [SwaggerOperation(operationId: "GetSingle")]
        public IActionResult Get(int id)
        {
            var response = _manager.GetSingle(id);

            // TODO image/jpeg or other type saved in db
            // this needs to be tested by swagger client
            return File(response.Photo, "image/jpeg");
        }
    }
}