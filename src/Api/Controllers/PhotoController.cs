namespace DetroitHarps.Api.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using DetroitHarps.Api.Services;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(OperationId = "CreatePhoto")]
        public ActionResult<int> Create([FromForm] PhotoUpload upload)
        {
            var photo = GetPhotoModel(upload);
            var result = _photoManager.Create(photo);
            return Ok(result);
        }

        [HttpPut("Update")]
        [SwaggerOperation(OperationId = "UpdatePhoto")]
        public ActionResult Update([FromBody] PhotoDisplayPropertiesDetailModel model)
        {
            _photoManager.UpdateDisplayProperties(model);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(OperationId = "DeletePhoto")]
        public ActionResult Delete([FromRoute] int id)
        {
            _photoManager.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        [SwaggerOperation(OperationId = "GetAllPhotos")]
        public ActionResult<IEnumerable<PhotoDisplayPropertiesDetailModel>> GetAll()
        {
            var result = _photoManager.GetAll();
            return Ok(result);
        }

        // TODO: resolve how this should be returned by swagger
        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        [SwaggerOperation(OperationId = "GetPhoto")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public FileResult Get([FromRoute] int id)
        {
            var result = _photoManager.GetPhotoBytes(id);
            return File(result.Data, result.MimeType);
        }

        // TODO: mapping logic should not be in ctor
        private static PhotoModel GetPhotoModel(PhotoUpload model)
        {
            if (model?.File == null)
            {
                return new PhotoModel { DisplayProperties = model?.DisplayProperties };
            }

            using (var ms = new MemoryStream())
            {
                model.File.CopyTo(ms);
                return new PhotoModel
                {
                    DisplayProperties = model.DisplayProperties,
                    Data = new PhotoDataModel
                    {
                        Data = ms.ToArray(),
                        MimeType = model.File.ContentType
                    }
                };
            }
        }

        // TODO: this class needs to be somewhere else
        public class PhotoUpload
        {
            public PhotoDisplayPropertiesModel DisplayProperties { get; set; }

            public IFormFile File { get; set; }
        }
    }
}