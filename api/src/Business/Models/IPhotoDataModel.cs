namespace Business.Models
{
    using System;
    
    public interface IPhotoDataModel
    {
        string MimeType { get; set; }

        byte[] Data { get; set; }
    }
} 