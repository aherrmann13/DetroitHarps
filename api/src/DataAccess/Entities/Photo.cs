namespace DataAccess.Entities
{
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Interfaces;

    public class Photo : IHasId
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