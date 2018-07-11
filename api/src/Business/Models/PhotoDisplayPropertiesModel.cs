namespace Business.Models
{
    using System;

    public class PhotoDisplayPropertiesModel : IPhotoDisplayPropertiesModel, IModelWithId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public int PhotoGroupId { get; set; }
    }
}