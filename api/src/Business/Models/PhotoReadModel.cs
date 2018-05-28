namespace Business.Models
{
    using System;

    public class PhotoReadModel : PhotoModelBase
    {
        public int Id { get; set; }
        
        public byte[] Photo { get; set; } 
    }
}