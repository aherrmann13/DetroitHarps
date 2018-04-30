namespace Repository.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Repository.Interfaces;

    public class PhotoGroup : IHasId
    {
        public PhotoGroup()
        {
            Photos = new List<Photo>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public IList<Photo> Photos { get; set;}
    }
}