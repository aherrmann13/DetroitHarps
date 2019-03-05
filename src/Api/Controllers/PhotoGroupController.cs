namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Tools;

    [Route("[Controller]")]
    public class PhotoGroupController : Controller
    {
        private IPhotoGroupManager _photoGroupManager;

        public PhotoGroupController(IPhotoGroupManager photoGroupManager)
        {
            Guard.NotNull(photoGroupManager, nameof(photoGroupManager));

            _photoGroupManager = photoGroupManager;
        }

        [HttpPost("Create")]
        [SwaggerOperation(OperationId = "CreatePhotoGroup")]
        public ActionResult<int> Create([FromBody] PhotoGroupCreateModel model)
        {
            var result = _photoGroupManager.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        [SwaggerOperation(OperationId = "UpdatePhotoGroup")]
        public ActionResult Update([FromBody] PhotoGroupModel model)
        {
            _photoGroupManager.Update(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(OperationId = "DeletePhotoGroup")]
        public ActionResult Delete([FromRoute] int id)
        {
            _photoGroupManager.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        [SwaggerOperation(OperationId = "GetAllPhotoGroups")]
        public ActionResult<IEnumerable<PhotoGroupModel>> GetAll()
        {
            var result = _photoGroupManager.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        [SwaggerOperation(OperationId = "GetPhotoGroup")]
        public ActionResult<PhotoGroupModel> Get([FromRoute] int id)
        {
            var result = _photoGroupManager.Get(id);
            return Ok(result);
        }
    }
}