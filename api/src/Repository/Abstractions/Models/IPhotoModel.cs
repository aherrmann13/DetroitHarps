namespace Repository.Abstractions.Models
{
    public interface IPhotoModel
    {
        string Title { get; set; }

        int GroupId { get; set; }

        int SortOrder { get; set; }
    }
}