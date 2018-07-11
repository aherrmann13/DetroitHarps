namespace Business.Models
{
    using System;

    public class PhotoGroupModel : IModelWithId, IPhotoGroupModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int SortOrder { get; set; }
        
    }
}