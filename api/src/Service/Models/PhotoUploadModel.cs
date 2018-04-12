namespace Service.Models
{
    using Microsoft.AspNetCore.Http;
    
    public class PhotoUploadModel
    {
        public string Title { get; set; }

        public int GroupId { get; set; }

        public int SortOrder {get; set;}

        public IFormFile Photo { get; set; }
    }
}