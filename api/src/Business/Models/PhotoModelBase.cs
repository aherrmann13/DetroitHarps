namespace Business.Models
{
    using System;
    
    public abstract class PhotoModelBase
    {
        public string Title { get; set; }

        public int GroupId { get; set; }

        public int SortOrder { get; set; }
    }
}