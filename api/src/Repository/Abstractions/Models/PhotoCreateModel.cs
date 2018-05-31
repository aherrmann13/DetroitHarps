namespace Repository.Abstractions.Models
{
    using System;

    public class PhotoCreateModel : IPhotoModel
    {
        public string Title { get; set; }

        public int GroupId { get; set; }

        public int SortOrder { get; set; }

        public byte[] Data { get; set; }
    }
}