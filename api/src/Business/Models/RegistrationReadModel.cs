namespace Business.Models
{
    public class RegistrationReadModel : RegistrationModelBase
    {
        public int Id { get; set; }

        public bool HasPaid { get; set; }
    }
}