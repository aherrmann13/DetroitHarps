namespace Repository.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    public interface IHasId
    {
        [Key]
        int Id { get; set; }
    }
}