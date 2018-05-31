namespace Repository.Abstractions.Models
{
    using System;

    public class PhotoGroupUpdateModel : IPhotoGroupModel, IModelWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int SortOrder { get; set; }
        
    }
}