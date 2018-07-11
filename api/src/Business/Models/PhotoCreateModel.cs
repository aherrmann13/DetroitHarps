namespace Business.Models
{
    using System;

    public class PhotoCreateModel : IPhotoDisplayPropertiesModel, IPhotoDataModel
    {
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public int PhotoGroupId { get; set; }
        public string MimeType { get; set; }
        public byte[] Data { get; set; }
        
    }
}