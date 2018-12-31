namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<int> Create([FromBody] PhotoGroupCreateModel model)
        {
            var result = _photoGroupManager.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] PhotoGroupModel model)
        {
            _photoGroupManager.Update(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete([FromQuery] int id)
        {
            _photoGroupManager.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PhotoGroupModel>> GetAll()
        {
            var result = _photoGroupManager.GetAll();
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public ActionResult<PhotoGroupModel> Get(int id)
        {
            var result = _photoGroupManager.Get(id);
            return Ok(result);
        }
    }
}