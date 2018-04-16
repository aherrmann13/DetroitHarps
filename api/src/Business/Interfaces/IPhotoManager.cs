namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IPhotoManager
    {
        int Create(PhotoCreateModel model);

        IEnumerable<int> Update(params PhotoMetadataUpdateModel[] models);

        IEnumerable<int> Delete(params int[] ids);

        IEnumerable<PhotoMetadataReadModel> GetAll();

        IEnumerable<PhotoMetadataReadModel> Get(params int[] ids);

        PhotoReadModel GetSingle(int id);

    }
}