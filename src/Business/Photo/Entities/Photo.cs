namespace DetroitHarps.Business.Photo.Entities
{
    public class Photo : IHasId<int>
    {
        public int Id { get; set; }

        public PhotoData Data { get; set; }

        public PhotoDisplayProperties DisplayProperties { get; set; }
    }
}