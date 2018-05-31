namespace Repository.Abstractions
{
    using System.Collections.Generic;
    using Repository.Abstractions.Models;
    
    public interface IPhotoGroupRepository
    {
        int Create(PhotoGroupCreateModel model);

        void Update(PhotoGroupUpdateModel model);

        void Delete(int id);

        IEnumerable<PhotoGroupReadModel> GetAll();

        IEnumerable<PhotoGroupReadModel> Get(int id);
    }
}