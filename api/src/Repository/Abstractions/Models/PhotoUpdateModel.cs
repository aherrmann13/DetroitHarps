namespace Repository.Abstractions.Models
{
    using System;

    public class PhotoUpdateModel : IPhotoModel, IModelWithId
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int GroupId { get; set; }

        public int SortOrder { get; set; }
        
    }
}