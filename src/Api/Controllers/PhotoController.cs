namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.AspNetCore.Mvc;
    using Tools;

    [Route("[Controller]")]
    public class PhotoController : Controller
    {
        private IPhotoManager _photoManager;

        public PhotoController(IPhotoManager photoManager)
        {
            Guard.NotNull(photoManager, nameof(photoManager));

            _photoManager = photoManager;
        }

        [HttpPost("Create")]
        public ActionResult<int> Create([FromBody] PhotoModel model)
        {
            var result = _photoManager.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public ActionResult Update([FromBody] PhotoDisplayPropertiesDetailModel model)
        {
            _photoManager.UpdateDisplayProperties(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _photoManager.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PhotoDisplayPropertiesDetailModel>> GetAll()
        {
            var result = _photoManager.GetAll();
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public ActionResult<PhotoDataModel> Get([FromRoute] int id)
        {
            var result = _photoManager.GetPhotoBytes(id);
            return Ok(result);
        }
    }
}