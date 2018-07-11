namespace Business.Models
{
    using System;
    
    public interface IPhotoDisplayPropertiesModel
    {
        string Title { get; set; }

        int SortOrder { get; set; }

        int PhotoGroupId { get; set; }
    }
} 