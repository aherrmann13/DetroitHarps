namespace Business.Models
{
    using System;
    using System.Collections.Generic;

    public class PhotoGroupReadModel : PhotoGroupModelBase
    {
        public PhotoGroupReadModel()
        {
            PhotoIds = new List<int>();
        }

        public int Id { get; set; }

        public IList<int> PhotoIds { get; set; }

        public object ToList()
        {
            throw new NotImplementedException();
        }
    }
}