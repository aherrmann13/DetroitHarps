namespace Repository.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}