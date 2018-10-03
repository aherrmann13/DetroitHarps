namespace DetroitHarps.Business.Photo
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Photo.Models;

    public interface IPhotoGroupManager
    {
        int Create(PhotoGroupCreateModel model);

        void Update(PhotoGroupModel model);

        void Delete(int id);

        IEnumerable<PhotoGroupModel> GetAll();

        PhotoGroupModel Get(int id);
    }
}