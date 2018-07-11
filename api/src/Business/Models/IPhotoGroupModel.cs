namespace Business.Models
{
    using System;
    
    public interface IPhotoGroupModel
    {
        string Name { get; set; }

        int SortOrder { get; set; }
    }
} 