namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IPhotoGroupManager
    {
        int Create(PhotoGroupCreateModel model);

        int Update(PhotoGroupUpdateModel model);

        int Delete(int id);

        IEnumerable<PhotoGroupReadModel> GetAll();

        PhotoGroupReadModel Get(int id);
    }
}