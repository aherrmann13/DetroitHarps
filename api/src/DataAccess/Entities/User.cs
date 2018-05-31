namespace DataAccess.Entities
{
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Interfaces;

    public class User : IHasId
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}