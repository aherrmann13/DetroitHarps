namespace Repository.Abstractions.Models
{
    using System;

    public class PhotoGroupCreateModel : IPhotoGroupModel
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
    }
}