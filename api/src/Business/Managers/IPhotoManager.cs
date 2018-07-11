namespace Business.Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IPhotoManager
    {
        int Create(PhotoCreateModel model);

        void UpdateDisplayProperties(PhotoDisplayPropertiesModel model);

        void Delete(int id);

        IEnumerable<PhotoDisplayPropertiesModel> GetAll();

        IEnumerable<PhotoDisplayPropertiesModel> GetByGroupId(int groupId);

        PhotoDisplayPropertiesModel Get(int id);

        PhotoDataModel GetPhotoBytes(int id);

    }
}