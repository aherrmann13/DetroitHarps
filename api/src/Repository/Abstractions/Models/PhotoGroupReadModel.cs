namespace Repository.Abstractions.Models
{
    using System;
    using System.Collections.Generic;

    public class PhotoGroupReadModel : IPhotoGroupModel, IModelWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int SortOrder { get; set; }

        public IList<int> PhotoIds { get; set; }
    }
}