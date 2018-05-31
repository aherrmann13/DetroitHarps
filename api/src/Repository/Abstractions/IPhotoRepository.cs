namespace Repository.Abstractions
{
    using System.Collections.Generic;
    using Repository.Abstractions.Models;
    
    public interface IPhotoRepository
    {
        int Create(PhotoCreateModel model);

        void Update(PhotoUpdateModel model);

        void Delete(int id);

        IEnumerable<PhotoReadModel> GetAll();

        IEnumerable<PhotoReadModel> Get(int id);

        byte[] GetFile(int id);
    }
}