namespace Business.Entities
{
    public class PhotoDisplayProperties : IHasId
    {
        public int Id { get; set; }

        public int PhotoGroupId { get; set; }

        public PhotoData PhotoData { get; set; }

        public string Title { get; set; }

        public int SortOrder { get; set; }
    }
}