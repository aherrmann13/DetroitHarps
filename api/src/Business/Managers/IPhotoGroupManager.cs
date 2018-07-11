namespace Business.Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IPhotoGroupManager
    {
        int Create(PhotoGroupCreateModel model);

        void Update(PhotoGroupModel model);

        void Delete(int id);

        IEnumerable<PhotoGroupModel> GetAll();

        PhotoGroupModel Get(int id);

    }
}