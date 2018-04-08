namespace Business.Models
{
    using System.Collections.Generic;
    
    public class PhotoGroupReadModel : PhotoGroupModelBase
    {
        public PhotoGroupReadModel()
        {
            PhotoIds = new List<int>();
        }

        public int Id { get; set; }

        public IList<int> PhotoIds { get; set; }
    }
}