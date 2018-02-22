namespace Repository
{
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        [Key]
        public int Id { get; set; }

        public int PhotoGroupId { get; set; }

        public PhotoGroup PhotoGroup { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public int SortOrder { get; set; }

        
    }
}