namespace Business.Models
{
    using System;

    public class PhotoDataModel : IPhotoDataModel
    {
        public string MimeType { get; set; }
        public byte[] Data { get; set; }
    }
}