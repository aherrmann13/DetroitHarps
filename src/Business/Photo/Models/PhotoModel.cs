namespace DetroitHarps.Business.Photo.Models
{
    public class PhotoModel
    {
        public PhotoDisplayPropertiesModel DisplayProperties { get; set; } = new PhotoDisplayPropertiesModel();

        public PhotoDataModel Data { get; set; } = new PhotoDataModel();
    }
}