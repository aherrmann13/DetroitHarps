namespace DetroitHarps.Business.Photo
{
    using System.Collections;
    using System.Collections.Generic;
    using DetroitHarps.Business.Photo.Models;

    public interface IPhotoManager
    {
        int Create(PhotoModel model);

        void UpdateDisplayProperties(PhotoDisplayPropertiesDetailModel model);

        void Delete(int id);

        IEnumerable<PhotoDisplayPropertiesDetailModel> GetAll();

        PhotoDataModel GetPhotoBytes(int id);
    }
}