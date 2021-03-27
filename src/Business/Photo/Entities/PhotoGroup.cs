namespace DetroitHarps.Business.Photo.Entities
{
    public class PhotoGroup : IHasId<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }
    }
}