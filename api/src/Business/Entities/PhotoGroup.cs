namespace Business.Entities
{
    using System.Collections.Generic;

    public class PhotoGroup : IHasId
    {
        public PhotoGroup()
        {
            Photos = new List<PhotoDisplayProperties>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public IList<PhotoDisplayProperties> Photos { get; set;}
    }
}