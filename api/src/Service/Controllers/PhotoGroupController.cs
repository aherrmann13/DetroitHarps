namespace Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Business.Interfaces;
    using Business.Models;
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

        ////[HttpPost("Create")]
        ////[Produces("application/json", Type = typeof(int))]
        ////[SwaggerOperation(operationId: "CreatePhotoGroup")]
        ////public IActionResult Create([FromBody] PhotoGroupCreateModel model)
        ////{
        ////    var response = _manager.Create(model);
        ////
        ////    return Json(response);
        ////}
        ////
        ////[HttpPost("Update")]
        ////[Produces("application/json", Type = typeof(int))]
        ////[SwaggerOperation(operationId: "UpdatePhotoGroup")]
        ////public IActionResult Update(PhotoGroupUpdateModel model)
        ////{
        ////    var response = _manager.Update(model);
        ////
        ////    return Json(response);
        ////}
        ////
        ////// TODO : make this consistant with other deletes
        ////[HttpDelete("Delete/{id}")]
        ////[Produces("application/json", Type = typeof(int))]
        ////[SwaggerOperation(operationId: "DeletePhotoGroup")]
        ////public IActionResult Delete(int id)
        ////{
        ////    var response = _manager.Delete(id);
        ////
        ////    return Json(response);
        ////}

        [HttpGet("GetAll")]
        [Produces("application/json", Type = typeof(IList<PhotoGroupReadModel>))]
        [SwaggerOperation(operationId: "GetAllPhotoGroups")]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll().ToList();

            return Json(response);
        }

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