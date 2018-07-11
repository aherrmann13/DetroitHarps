namespace Business.Entities
{
    public class User : IHasId
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}