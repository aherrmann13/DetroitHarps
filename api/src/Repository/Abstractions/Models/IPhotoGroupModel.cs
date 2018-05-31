namespace Repository.Abstractions.Models
{
    public interface IPhotoGroupModel
    {
        string Name { get; set; }

        int SortOrder { get; set; }
    }
}