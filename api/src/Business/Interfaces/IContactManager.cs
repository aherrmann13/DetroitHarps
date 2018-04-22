namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IContactManager
    {
        void Contact(ContactModel model);

        void Contact(string subject, string message);

    }
}