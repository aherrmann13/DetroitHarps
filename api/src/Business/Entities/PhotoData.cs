namespace Business.Entities
{
    public class PhotoData : IHasId
    {
        public int Id { get; set; }

        public string MimeType { get; set; }

        public byte[] Data { get; set; }
    }
}