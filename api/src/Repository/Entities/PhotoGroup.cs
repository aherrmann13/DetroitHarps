namespace Repository
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PhotoGroup
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