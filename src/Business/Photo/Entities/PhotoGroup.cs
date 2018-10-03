namespace DetroitHarps.Business.Photo.Entities
{
    using System.Collections.Generic;

    public class PhotoGroup : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }
    }
}