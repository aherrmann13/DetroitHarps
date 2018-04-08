namespace Business.Models
{
    using System;
    
    public abstract class PhotoGroupModelBase
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }
    }
}