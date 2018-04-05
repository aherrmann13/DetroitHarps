namespace Repository.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        [Key]
        public int Id { get; set; }

        public int PhotoGroupId { get; set; }

        public PhotoGroup PhotoGroup { get; set; }

        public string Title { get; set; }

        public int SortOrder { get; set; }

        // TODO : move to seperate entity for performance
        public byte[] Data { get; set; }
    }
}