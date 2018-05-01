namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IPhotoManager
    {
        int Create(PhotoCreateModel model);

        int Update(PhotoMetadataUpdateModel models);

        int Delete(int id);

        IEnumerable<PhotoMetadataReadModel> GetMetadata();

        PhotoMetadataReadModel GetMetadata(int id);

        PhotoReadModel Get(int id);

    }
}